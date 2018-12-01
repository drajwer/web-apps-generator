using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Generator.Generator;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class WebApiProjectGenerator : BaseGenerator
    {
        private readonly SolutionPathService _pathService;
        
        public WebApiProjectGenerator(IGeneratorConfiguration generatorConfiguration) : base(generatorConfiguration)
        {
            _pathService = new SolutionPathService(generatorConfiguration);
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            var controllerFileInfo = new FileInfo() { NameTemplate = "{{Entity.Name}}Controller.cs", TemplatePath = "WebApi.Controller.liquid" };
            GenerateControllers(entities, controllerFileInfo);
        }

        private void GenerateControllers(IEnumerable<Entity> entities, FileInfo controllerFileInfo)
        {
            foreach (var entity in entities)
            {
                var nameTemplate = controllerFileInfo.NameTemplate;
                // Generate Controller
            }
        }
    }
}
