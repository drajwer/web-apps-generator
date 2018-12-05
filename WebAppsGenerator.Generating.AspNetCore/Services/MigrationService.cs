using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class MigrationService
    {
        private readonly SolutionPathService _pathService;
        private readonly IFileService _fileService;
        private readonly ICommandLineService _commandLineService;

        public MigrationService(SolutionPathService pathService, IFileService fileService, ICommandLineService commandLineService)
        {
            _pathService = pathService;
            _fileService = fileService;
            _commandLineService = commandLineService;
        }

        public void AddMigration(string migrationName)
        {
            _commandLineService.RunCommand($"dotnet ef migrations add --startup-project {_pathService.WebApiDirPath} --project {_pathService.CoreDirPath} {migrationName}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetMigrationName">Empty target migration name applies all existing migrations</param>
        public void UpdateDatabase(string targetMigrationName = "")
        {
            _commandLineService.RunCommand($"dotnet ef database update {targetMigrationName}");
        }
    }
}
