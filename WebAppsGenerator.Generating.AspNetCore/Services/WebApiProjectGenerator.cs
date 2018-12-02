using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;
using WebAppsGenerator.Generator.Generator;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class WebApiProjectGenerator : BaseGenerator
    {
        private readonly SolutionPathService _pathService;
        private readonly IFileService _fileService;

        public WebApiProjectGenerator(IGeneratorConfiguration generatorConfiguration, IFileService fileService) : base(generatorConfiguration)
        {
            _fileService = fileService;
            _pathService = new SolutionPathService(generatorConfiguration);
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            GenerateControllers(entities);
        }

        private void GenerateControllers(IEnumerable<Entity> entities)
        {
            var controllerFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}Controller.cs",
                TemplatePath = "WebApi.Controller.liquid",
                OutputPath = Path.Combine(_pathService.WebApiDirPath, "Controllers")
            };

            var sampleControllerPath = Path.Combine(controllerFileInfo.OutputPath, "ValuesController.cs");
            File.Delete(sampleControllerPath);

            foreach (var entity in entities)
            {
                _fileService.CreateFromTemplate(controllerFileInfo, new SingleEntityDrop(GeneratorConfiguration, entity));
            }
        }
    }
}
