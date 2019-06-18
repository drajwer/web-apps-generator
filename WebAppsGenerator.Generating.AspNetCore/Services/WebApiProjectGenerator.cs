using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class WebApiProjectGenerator : BaseGenerator, IAspNetCoreChildGenerator
    {
        private readonly SolutionPathService _solutionPathService;

        public WebApiProjectGenerator(IGeneratorConfiguration generatorConfiguration, IAspNetCoreFileService fileService,
            IWebApiProjectTemplatingConfigProvider templatingConfigProvider, CSharpDropFactory dropFactory, SolutionPathService solutionPathService)
            : base(generatorConfiguration, dropFactory, templatingConfigProvider, fileService)
        {
            _solutionPathService = solutionPathService;
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            RemoveUnnecessaryFiles();

            base.Generate(entities);
        }

        public void RemoveUnnecessaryFiles()
        {
            File.Delete(Path.Combine(_solutionPathService.WebApiDirPath, "Controllers/ValuesController.cs"));
        }
    }
}
