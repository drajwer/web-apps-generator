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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.IntegrationTests.Extensions;
using WebAppsGenerator.IntegrationTests.Helpers;

namespace WebAppsGenerator.IntegrationTests
{
    public class SamplesTestBase
    {
        private List<Process> _runProcesses;
        private GeneratorOptions _options;
        private readonly CommandLineService _commandLineService;
        
        private static readonly string In = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-In");
        private static readonly string Out = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-Out");
        private const string ConfigJsonFileName = "config.json";

        private static string ConfigJsonFilePath => Path.Combine(In, ConfigJsonFileName);
        private string PathToWebApiProject => Path.Combine(Out, "WebApi", $"{_options.ProjectName}.WebApi");
        private string PathToWebUiProject => Path.Combine(Out, "WebUI", $"{_options.ProjectName.ToLower()}.web");
        
        public SamplesTestBase()
        {
            void Func(string s) => Debug.WriteLine(s);
            _commandLineService = new CommandLineService(Func,Func);
            
            _runProcesses = new List<Process>();
        }

        private void Setup(string directoryName)
        {
            EnsureRunningOnWindows();
            _runProcesses = new List<Process>();
            
            using (var stream = CreateConfigFileResourceStream(directoryName))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                SetupGeneratorOptions(directoryName, json);

                Directory.CreateDirectory(In);
                Directory.CreateDirectory(Out);
                
                WriteGeneratorOptionsToInputFile();
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

        protected async Task RunIntegrationTest(string directoryName, string sampleCollectionName)
        {
            using (var writer = new DebugWriter())
            {
                System.Console.SetOut(writer);

                // Arrange
                Setup(directoryName);

                // Act
                Console.Program.Main(new[] {ConfigJsonFilePath});

                // Assert
                var webApiReturnCode = RunDotnetBuild();
                RunDotnetWebApi();
                string json = await CallWebApi(sampleCollectionName);
                var webUiReturnCode = RunWebUiBuild();

                Assert.AreEqual(0, webApiReturnCode);
                Assert.AreEqual(0, webUiReturnCode);
                Assert.AreEqual("[]", json);
            }
        }
        
        private static Stream CreateConfigFileResourceStream(string directoryName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"WebAppsGenerator.IntegrationTests.Samples.{directoryName}.{ConfigJsonFileName}";
            
            return assembly.GetManifestResourceStream(resourceName);
        }

        private static void EnsureRunningOnWindows()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException("Integration tests work only on Windows NT or higher");
        }

        private void WriteGeneratorOptionsToInputFile()
        {
            var json = JsonConvert.SerializeObject(_options);
            File.WriteAllText(ConfigJsonFilePath, json);
        }

        private void SetupGeneratorOptions(string directoryName, string json)
        {
            var assembly = Assembly.GetExecutingAssembly();

            _options = JsonConvert.DeserializeObject<GeneratorOptions>(json);
            var codePath = Path.GetFullPath(Path.Combine(assembly.Location, "..", "..", "..", ".."));
            _options.InputPath = Path.Combine(codePath, "Samples", directoryName);
            _options.OutputPath = Out;
        }

        private int RunWebUiBuild()
        {
            var webUiReturnCode =
                _commandLineService.RunCommand($"cd {PathToWebUiProject} && npm install && npm run build");
            return webUiReturnCode;
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
        

        private async Task<string> CallWebApi(string sampleCollectionName)
        {
            string body = null;
            HttpStatusCode statusCode;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:5001/api/{sampleCollectionName}");
                if(response.IsSuccessStatusCode)
                    body = await response.Content.ReadAsStringAsync();
            }

            return body;
        }
    }
}
