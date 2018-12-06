using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class CoreProjectGenerator : BaseGenerator
    {
        private readonly SolutionPathService _pathService;
        private readonly IFileService _fileService;
        private readonly MigrationService _migrationService;

        public CoreProjectGenerator(IGeneratorConfiguration generatorConfiguration, IFileService fileService,
            MigrationService migrationService) : base(generatorConfiguration)
        {
            _fileService = fileService;
            _migrationService = migrationService;
            _pathService = new SolutionPathService(generatorConfiguration);
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            var sampleControllerPath = Path.Combine(_pathService.CoreDirPath, "Class1.cs");
            File.Delete(sampleControllerPath);

            GenerateModels(entities);
            GenerateDbContext(entities);
            GenerateRepository(entities);

            _migrationService.AddMigration("Init");
            _migrationService.UpdateDatabase();
        }

        private void GenerateModels(IEnumerable<Entity> entities)
        {
            var modelFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}.cs",
                TemplatePath = "Core.Entity.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Models")
            };

            foreach (var entity in entities)
            {
                _fileService.CreateFromTemplate(modelFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, entity));
            }
        }

        private void GenerateDbContext(IEnumerable<Entity> entities)
        {
            var modelFileInfo = new FileInfo
            {
                NameTemplate = "AppDbContext.cs",
                TemplatePath = "Core.AppDbContext.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Context")
            };

            _fileService.CreateFromTemplate(modelFileInfo, new EntityListDrop(GeneratorConfiguration, _pathService, entities));
        }

        private void GenerateRepository(IEnumerable<Entity> entities)
        {
            var modelFileInfo = new FileInfo
            {
                NameTemplate = "Repository.cs",
                TemplatePath = "Core.Repository.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Context")
            };

            _fileService.CreateFromTemplate(modelFileInfo, new EntityListDrop(GeneratorConfiguration, _pathService, entities));
        }
    }
}
