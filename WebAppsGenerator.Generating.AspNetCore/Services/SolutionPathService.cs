using System.IO;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class SolutionPathService
    {
        private const string SolutionDirectoryName = "WebApi";
        private readonly IGeneratorConfiguration _generatorConfiguration;
        public SolutionPathService(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }

        public string SolutionDirPath => Path.Combine(_generatorConfiguration.OutputPath, SolutionDirectoryName);
        public string SolutionFilePath => Path.Combine(SolutionDirPath, $"{_generatorConfiguration.ProjectName}.sln");
        public string CoreDirPath => Path.Combine(SolutionDirPath, CoreProjectName);
        public string WebApiDirPath => Path.Combine(SolutionDirPath, WebApiProjectName);

        public string WebApiProjectName => $"{_generatorConfiguration.ProjectName}.WebApi";
        public string CoreProjectName => $"{_generatorConfiguration.ProjectName}.Core";
    }
}
