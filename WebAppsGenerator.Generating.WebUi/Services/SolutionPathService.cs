﻿using System.IO;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    /// <summary>
    /// Provides paths for specific files or directories in web UI project
    /// </summary>
    public class SolutionPathService
    {
        private const string WebProjectDirectoryName = "WebUI";
        private readonly IGeneratorConfiguration _generatorConfiguration;

        public SolutionPathService(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }
        public string OutputPath => _generatorConfiguration.OutputPath;
        public string WebUiDirPath => Path.Combine(_generatorConfiguration.OutputPath, WebProjectDirectoryName);
        public string WebProjectDirPath => Path.Combine(WebUiDirPath, WebUiProjectName);

        public string WebUiProjectName => $"{_generatorConfiguration.ProjectName.ToLower()}.web";

        public string SrcDir => Path.Combine(WebProjectDirPath, "src");
        public string ScreensDir => Path.Combine(SrcDir, "screens");
        public string ComponentsDir => Path.Combine(SrcDir, "components");
    }
}
