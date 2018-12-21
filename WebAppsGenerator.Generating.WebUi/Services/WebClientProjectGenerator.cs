using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebClientProjectGenerator : IGenerator
    {
        private const bool IsEnabled = true;
        private const bool CreateReactApp = false;

        private readonly SolutionPathService _pathService;
        private readonly ICommandLineService _commandLineService;
        private readonly IGenerator _webUiGenerator;

        public WebClientProjectGenerator(SolutionPathService pathService, ICommandLineService commandLineService, IGenerator webUiGenerator)
        {
            _pathService = pathService;
            _commandLineService = commandLineService;
            _webUiGenerator = webUiGenerator;
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

            _webUiGenerator.Generate(entities);
        }

        private void CreateApp()
        {
            _commandLineService.RunCommand("npm i create-react-app -g");
            _commandLineService.RunCommand($"npx create-react-app {_pathService.WebProjectDirPath} --scripts-version=react-scripts-ts");
            _commandLineService.RunCommand("npm install --save @types/history");
            _commandLineService.RunCommand("npm install --save react-router-dom");
            _commandLineService.RunCommand("npm install --save @types/react-router-dom"); 
            _commandLineService.RunCommand("npm install --save @types/lodash");
            _commandLineService.RunCommand("npm install --save @types/jquery");
            _commandLineService.RunCommand("npm install --save jquery");
            _commandLineService.RunCommand("npm install --save @material-ui/core");
            _commandLineService.RunCommand("npm install --save @types/material-ui");
            _commandLineService.RunCommand("npm install --save @material-ui/icons");

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
