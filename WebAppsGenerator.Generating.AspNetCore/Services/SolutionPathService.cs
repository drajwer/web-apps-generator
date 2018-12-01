using System;
using System.Collections.Generic;
using System.Text;
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

        public string SolutionDirPath => $"{_generatorConfiguration.OutputPath }\\{ SolutionDirectoryName}";
        public string SolutionFilePath => $"{SolutionDirPath}\\{_generatorConfiguration.ProjectName}.sln";
        public string CoreDirPath => $"{SolutionDirPath}\\{CoreProjectName}";
        public string WebApiDirPath => $"{SolutionDirPath}\\{ WebApiProjectName}";

        public string WebApiProjectName => $"{_generatorConfiguration.ProjectName}.WebApi";
        public string CoreProjectName => $"{_generatorConfiguration.ProjectName}.Core";
    }
}
