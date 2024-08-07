﻿using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    /// <summary>
    /// Creates web client project and runs all registered web UI generators
    /// </summary>
    public class WebClientProjectGenerator : IGenerator
    {
        private readonly SolutionPathService _pathService;
        private readonly ICommandLineService _commandLineService;
        private readonly IEnumerable<IWebUiChildGenerator> _webUiGenerators;
        private readonly IGeneratorConfiguration _configuration;
        private readonly IOverwriteService _overwriteService;
        private readonly IWebUiFirstRunProvider _firstRunProvider;

        public WebClientProjectGenerator(SolutionPathService pathService, ICommandLineService commandLineService,
            IEnumerable<IWebUiChildGenerator> webUiGenerators, IGeneratorConfiguration configuration, IOverwriteService overwriteService, IWebUiFirstRunProvider firstRunProvider)
        {
            _pathService = pathService;
            _commandLineService = commandLineService;
            _webUiGenerators = webUiGenerators;
            _configuration = configuration;
            _overwriteService = overwriteService;
            _firstRunProvider = firstRunProvider;
        }

        /// <summary>
        /// Generates web client project
        /// </summary>
        public void Generate(IEnumerable<Entity> entities)
        {
            if(!_configuration.RunWebUiGen)
                return;

            var firstRun = _firstRunProvider.IsFirstRun();
            if (firstRun)
            {
                _overwriteService.SetOverwriteAll();
                CreateApp();
                RemoveUnnecessaryFiles();
            }

            foreach (var webUiChildGenerator in _webUiGenerators)
            {
                webUiChildGenerator.Generate(entities);
            }
            
            if(firstRun)
                _overwriteService.ResetOverwriteAll();
        }

        private void CreateApp()
        {
            _commandLineService.RunCommand($"npx create-react-app {_pathService.WebProjectDirPath} --scripts-version=react-scripts-ts", "y");
        }
        private bool IsFirstRun()
        {
            var webApiCsProjPath = Path.Combine(_pathService.WebProjectDirPath, "project.json");
            return !File.Exists(webApiCsProjPath);
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
    }
}
