using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class WebUiGenerator : BaseGenerator
    {
        private readonly ICommandLineService _commandLineService;
        private readonly IFileService _fileService;
        private readonly SolutionPathService _pathService;

        public WebUiGenerator(IGeneratorConfiguration generatorConfiguration,
            ICommandLineService commandLineService, SolutionPathService pathService, IFileService fileService) : base(generatorConfiguration)
        {
            _commandLineService = commandLineService;
            _pathService = pathService;
            _fileService = fileService;
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            CreateApp();
            RemoveUnnecessaryFiles();
            AddBasicFiles();
            AddManyTemplates("screens", _pathService.ScreensDir);
            AddManyTemplates("components", _pathService.ComponentsDir);
        }

        private void CreateApp()
        {
            _commandLineService.RunCommand("npm i create-react-app -g");
            _commandLineService.RunCommand($"npx create-react-app {_pathService.WebProjectDirPath} --scripts-version=react-scripts-ts");
            _commandLineService.RunCommand("npm install --save @types/history");
            _commandLineService.RunCommand("npm install --save react-router-dom");
            _commandLineService.RunCommand("npm install --save @types/react-router-dom");
        }

        private void RemoveUnnecessaryFiles()
        {
            var files = new List<string> { "App.tsx", "App.css", "App.test.tsx", "index.css", "index.tsx", "logo.svg" };
            foreach (var file in files)
            {
                var path = Path.Combine(_pathService.SrcDir, file);
                File.Delete(path);
            }
        }
        /// <summary>
        /// Renders all templates from all subfolders of the specified directory into respective subfolders of the output directory.
        /// </summary>
        /// <param name="templateMainDirectory">Directory which subfolders will be scanned for templates.</param>
        /// <param name="outputMainDirectory">Main directory for rendering subfolders files to.</param>
        private void AddManyTemplates(string templateMainDirectory, string outputMainDirectory)
        {
            var resources = Assembly.GetAssembly(typeof(WebUiGenerator)).GetManifestResourceNames().ToList();
            var screens = resources.Where(r => r.Contains(templateMainDirectory));
            foreach (var screen in screens)
            {
                var parts = screen.Split('.');
                var fileInfo = new FileInfo
                {
                    NameTemplate = $"{parts[parts.Length - 3]}.{parts[parts.Length - 2]}",
                    TemplatePath = screen.Substring(screen.IndexOf("WebUI", StringComparison.Ordinal)),
                    OutputPath = Path.Combine(outputMainDirectory, parts[parts.Length - 4])
                };
                _fileService.CreateFromTemplate(fileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
            }
        }

        private void AddBasicFiles()
        {
            var fileInfos = new List<FileInfo>
            {
                new FileInfo("index.tsx", "WebUI.src.index.tsx.liquid", _pathService.SrcDir),
                new FileInfo("Router.tsx", "WebUI.src.routing.Router.tsx.liquid", Path.Combine(_pathService.SrcDir, "routing")),
            };
            foreach (var fileInfo in fileInfos)
            {
                _fileService.CreateFromTemplate(fileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
            }

        }
    }
}
