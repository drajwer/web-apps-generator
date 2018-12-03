using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;
using WebAppsGenerator.Generator.Generator;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class CoreProjectGenerator : BaseGenerator
    {
        private readonly SolutionPathService _pathService;
        private readonly IFileService _fileService;


        public CoreProjectGenerator(IGeneratorConfiguration generatorConfiguration, IFileService fileService) : base(generatorConfiguration)
        {
            _fileService = fileService;
            _pathService = new SolutionPathService(generatorConfiguration);
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            var sampleControllerPath = Path.Combine(_pathService.CoreDirPath, "Class1.cs");
            File.Delete(sampleControllerPath);

            GenerateModels(entities);
        }

        private void GenerateModels(IEnumerable<Entity> entities)
        {
            var modelFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}.cs",
                TemplatePath = "Entity.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Models")
            };

            foreach (var entity in entities)
            {
                _fileService.CreateFromTemplate(modelFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, entity));
            }
        }
    }
}
