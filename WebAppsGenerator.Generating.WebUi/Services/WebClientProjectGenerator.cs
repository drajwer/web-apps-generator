using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebClientProjectGenerator : IGenerator
    {
        private const bool IsEnabled = true;
        private const bool CreateReactApp = false;

        private readonly SolutionPathService _pathService;
        private readonly ICommandLineService _commandLineService;
        private readonly IEnumerable<IWebUiChildGenerator> _webUiGenerators;

        public WebClientProjectGenerator(SolutionPathService pathService, ICommandLineService commandLineService, IEnumerable<IWebUiChildGenerator> webUiGenerators)
        {
            _pathService = pathService;
            _commandLineService = commandLineService;
            _webUiGenerators = webUiGenerators;
        }

        public void Generate(IEnumerable<Entity> entities)
        {
            if(!IsEnabled)
                return;

            if (CreateReactApp)
            {
                CreateApp();
                RemoveUnnecessaryFiles();
            }

            foreach (var webUiChildGenerator in _webUiGenerators)
            {
                webUiChildGenerator.Generate(entities);
            }
        }

        private void CreateApp()
        {
            _commandLineService.RunCommand("npm i create-react-app -g");
            _commandLineService.RunCommand($"npx create-react-app {_pathService.WebProjectDirPath} --scripts-version=react-scripts-ts");
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
