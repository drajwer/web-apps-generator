using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.IntegrationTests
{
    [TestClass]
    public class SamplesTests
    {
        private static string In = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-In");
        private static string Out = Path.Combine(Path.GetTempPath(), "WebAppsGen-IntTst-Out");
        private const string ConfigJsonFileName = "config.json";

        public string Setup([CallerMemberName] string directoryName = "")
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

                var newConfigFilePath = Path.Combine(In, ConfigJsonFileName);
                
                File.WriteAllText(newConfigFilePath, json);

                return newConfigFilePath;
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
            var inputPath = Setup();

            // Act
            Console.Program.Main(new [] {inputPath});

            // Assert
            
        }
    }
}
