using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.IntegrationTests
{
    [TestClass]
    public class SamplesTests
    {
        private static string In = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-In");
        private static string Out = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-Out");
        private const string ConfigJsonFileName = "config.json";
        private static string ConfigJsonFilePath => Path.Combine(In, ConfigJsonFileName);
        public GeneratorOptions Setup([CallerMemberName] string directoryName = "")
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"WebAppsGenerator.IntegrationTests.Samples.{directoryName}.{ConfigJsonFileName}";

            // TODO: change reader to use embedded resources
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var options = JsonConvert.DeserializeObject<GeneratorOptions>(json);
                var codePath = Path.GetFullPath(Path.Combine(assembly.Location, "..", "..", "..", ".."));
                options.InputPath = Path.Combine(codePath, "Samples", directoryName);
                options.OutputPath = Out;
                json = JsonConvert.SerializeObject(options);

                Directory.CreateDirectory(In);
                Directory.CreateDirectory(Out);

                File.WriteAllText(ConfigJsonFilePath, json);

                return options;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete(In, true);
            Directory.Delete(Out, true);

            // TODO: Delete database here
        }

        [TestMethod]
        public void Library()
        {
            // Arrange
            var options = Setup();

            // Act
            Console.Program.Main(new [] {ConfigJsonFilePath});
            Action<string> func =  s => Debug.WriteLine(s);

            var commandLineService = new CommandLineService(func, func);
            var pathToWebApiProject = Path.Combine(Out, "WebApi", $"{options.ProjectName}.WebApi");
            var returnCode = commandLineService.RunCommand($"dotnet build {pathToWebApiProject}");
            var webApiProcess = commandLineService.CreateProcessForCommand($"dotnet run --project {pathToWebApiProject}", true);

            // Assert
            Assert.AreEqual(0, returnCode);
            webApiProcess.Kill();
            webApiProcess.WaitForExit(5000);
        }
    }
}
