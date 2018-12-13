using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            GenerateCrudServices(entities);
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

            var joinModelFileInfo = new FileInfo(modelFileInfo)
            {
                OutputPath = Path.Combine(modelFileInfo.OutputPath, "Joins")
            };

            List<ModelDrop> drops = GetModelDrops(entities);

            foreach (var drop in drops)
            {
                var fileInfo = drop.IsJoinModel ? joinModelFileInfo : modelFileInfo;
                _fileService.CreateFromTemplate(fileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, drop));
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
            var drops = GetModelDrops(entities);
            _fileService.CreateFromTemplate(modelFileInfo, new EntityListDrop(GeneratorConfiguration, _pathService, drops));
        }

        private void GenerateRepository(IEnumerable<Entity> entities)
        {
            var implementationFileInfo = new FileInfo
            {
                NameTemplate = "Repository.cs",
                TemplatePath = "Core.Repository.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Context")
            };
            var interfaceFileInfo = new FileInfo
            {
                NameTemplate = "IRepository.cs",
                TemplatePath = "Core.IRepository.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Interfaces")
            };
            var setFileInfo = new FileInfo
            {
                NameTemplate = "RepositorySet.cs",
                TemplatePath = "Core.RepositorySet.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Services")
            };
            var modelDrops = entities.Select(e => new ModelDrop(e)).ToList();

            _fileService.CreateFromTemplate(implementationFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
            _fileService.CreateFromTemplate(interfaceFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
            _fileService.CreateFromTemplate(setFileInfo, new EntityListDrop(GeneratorConfiguration, _pathService, modelDrops));
        }

        private void GenerateCrudServices(IEnumerable<Entity> entities)
        {
            var implementationFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}CrudService.cs",
                TemplatePath = "Core.CrudService.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Services")
            };
            var baseFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}BaseCrudService.cs",
                TemplatePath = "Core.BaseCrudService.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Services", "Generated")
            };
            var interfaceFileInfo = new FileInfo
            {
                NameTemplate = "ICrudService.cs",
                TemplatePath = "Core.ICrudService.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Interfaces")
            };
            var drops = GetModelDrops(entities);


            _fileService.CreateFromTemplate(interfaceFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));

            foreach (var modelDrop in drops.Where(d => !d.IsJoinModel))
            {
                _fileService.CreateFromTemplate(baseFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, modelDrop));
                _fileService.CreateFromTemplate(implementationFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, modelDrop));
            }
        }

        private void GenerateTransactionScope()
        {
            var interfaceFileInfo = new FileInfo
            {
                NameTemplate = "ITransactionScope.cs",
                TemplatePath = "Core.ITransactionScope.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Interfaces")
            };
            var serviceFileInfo = new FileInfo
            {
                NameTemplate = "TransactionScope.cs",
                TemplatePath = "Core.TransactionScope.liquid",
                OutputPath = Path.Combine(_pathService.CoreDirPath, "Services")
            };

            _fileService.CreateFromTemplate(interfaceFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
            _fileService.CreateFromTemplate(serviceFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
        }



        private static List<ModelDrop> GetModelDrops(IEnumerable<Entity> entities)
        {
            var service = new ModelService();
            var drops = service.CreateModelDrops(entities);
            return drops;
        }
    }
}
