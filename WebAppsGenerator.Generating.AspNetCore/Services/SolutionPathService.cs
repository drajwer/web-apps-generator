using System.IO;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    /// <summary>
    /// Provides paths for specific files or directories in web api project
    /// </summary>
    public class SolutionPathService
    {
        private const string SolutionDirectoryName = "WebApi";
        private const string WebProjectDirectoryName = "WebUI";
        private readonly IGeneratorConfiguration _generatorConfiguration;
        public SolutionPathService(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }

        public string SolutionDirPath => Path.Combine(_generatorConfiguration.OutputPath, SolutionDirectoryName);
        public string WebUiDirPath => Path.Combine(_generatorConfiguration.OutputPath, WebProjectDirectoryName);
        public string SolutionFilePath => Path.Combine(SolutionDirPath, $"{_generatorConfiguration.ProjectName}.sln");
        public string CoreDirPath => Path.Combine(SolutionDirPath, CoreProjectName);
        public string WebApiDirPath => Path.Combine(SolutionDirPath, WebApiProjectName);
        public string WebProjectDirPath => Path.Combine(WebUiDirPath, WebUiProjectName);

        public string WebApiProjectName => $"{_generatorConfiguration.ProjectName}.WebApi";
        public string WebUiProjectName => $"{_generatorConfiguration.ProjectName.ToLower()}.web";
        public string CoreProjectName => $"{_generatorConfiguration.ProjectName}.Core";

        public string SrcDir => Path.Combine(WebProjectDirPath, "src");
        public string ScreensDir => Path.Combine(SrcDir, "screens");
        public string ComponentsDir => Path.Combine(SrcDir, "components");

    }
}
