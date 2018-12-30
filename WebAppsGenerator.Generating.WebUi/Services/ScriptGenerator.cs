using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Generating.WebUi.Interfaces;
using WebAppsGenerator.Generating.WebUi.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class ScriptGenerator : IWebUiChildGenerator
    {
        private readonly SolutionPathService _pathService;

        private readonly IWebUiFileService _fileService;
        private readonly IGeneratorConfiguration _generatorConfiguration;

        public ScriptGenerator(SolutionPathService pathService, IWebUiFileService fileService, IGeneratorConfiguration generatorConfiguration)
        {
            _pathService = pathService;
            _fileService = fileService;
            _generatorConfiguration = generatorConfiguration;
        }

        public void Generate(IEnumerable<Entity> entities)
        {
            var templatePath = "run.ps1.liquid";
            var outputPath = _pathService.OutputPath;
            var fileInfo = new FileInfo("run.ps1", templatePath, outputPath);
            _fileService.CreateFromTemplate(fileInfo, new WebUiBaseDrop(_pathService, _generatorConfiguration));
        }
    }
}
