using System.Collections.Generic;
using System.IO;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class CoreProjectGenerator : BaseGenerator
    {
        private readonly SolutionPathService PathService;
        private readonly MigrationService _migrationService;

        public CoreProjectGenerator(IGeneratorConfiguration generatorConfiguration, IAspNetCoreFileService fileService,
            MigrationService migrationService, ICoreProjectTemplatingConfigProvider templatingConfigProvider,
            CSharpDropFactory dropFactory) 
            : base(generatorConfiguration, dropFactory, templatingConfigProvider, fileService)
        {
            _migrationService = migrationService;
            PathService = new SolutionPathService(generatorConfiguration);
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            var sampleControllerPath = Path.Combine(PathService.CoreDirPath, "Class1.cs");
            File.Delete(sampleControllerPath);

            base.Generate(entities);

            _migrationService.AddMigration("Init");
            _migrationService.UpdateDatabase();
        }
    }
}
