using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DotLiquid;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.IntegrationTests.Extensions;

namespace WebAppsGenerator.IntegrationTests
{
    [TestClass]
    public class SamplesTests
    {
        private GeneratorOptions _options;
        private CommandLineService _commandLineService;
        private static string In = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-In");
        private static string Out = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-Out");
        private List<Process> _runProcesses;
        
        public SamplesTests()
        {
            Action<string> func =  s => Debug.WriteLine(s);
            _commandLineService = new CommandLineService(func,func);
            
            _runProcesses = new List<Process>();
        }

        private const string ConfigJsonFileName = "config.json";
        private static string ConfigJsonFilePath => Path.Combine(In, ConfigJsonFileName);
        private string PathToWebApiProject => Path.Combine(Out, "WebApi", $"{_options.ProjectName}.WebApi");
        private void Setup([CallerMemberName] string directoryName = "")
        {
            _runProcesses = new List<Process>();
            
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"WebAppsGenerator.IntegrationTests.Samples.{directoryName}.{ConfigJsonFileName}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                _options = JsonConvert.DeserializeObject<GeneratorOptions>(json);
                var codePath = Path.GetFullPath(Path.Combine(assembly.Location, "..", "..", "..", ".."));
                _options.InputPath = Path.Combine(codePath, "Samples", directoryName);
                _options.OutputPath = Out;
                json = JsonConvert.SerializeObject(_options);

                Directory.CreateDirectory(In);
                Directory.CreateDirectory(Out);

                File.WriteAllText(ConfigJsonFilePath, json);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var process in _runProcesses)
                process.KillTree();
            
            Directory.Delete(In, true);
            Directory.Delete(Out, true);

            var server = new Server("(localdb)\\MSSQLLocalDB");
            server.KillDatabase($"{_options.ProjectName}Db");
        }

        [TestMethod]
        public async Task Library()
        {
            // Arrange
            Setup();

            // Act
            Console.Program.Main(new [] {ConfigJsonFilePath});

            // Assert
            var returnCode = RunDotnetBuild();
            RunDotnetWebApi();
            string json = await CallWebApi();
            
            Assert.AreEqual(0, returnCode);
            Assert.AreEqual("[]", json);
        }

        private int RunDotnetBuild()
        {
            return _commandLineService.RunCommand($"dotnet build {PathToWebApiProject}");
        }
        
        private void RunDotnetWebApi()
        {
            var webApiProcess = _commandLineService.CreateProcessForCommand($"dotnet run --project {PathToWebApiProject}", true);
            _runProcesses.Add(webApiProcess);
            
            // Giving web api time to start
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
        

        private async Task<string> CallWebApi()
        {
            string body = null;
            HttpStatusCode statusCode;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:5001/api/books");
                if(response.IsSuccessStatusCode)
                    body = await response.Content.ReadAsStringAsync();
            }

            return body;
        }
    }
}
