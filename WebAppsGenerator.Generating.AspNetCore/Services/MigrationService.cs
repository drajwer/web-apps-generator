﻿using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    /// <summary>
    /// Provides creating and running database migrations
    /// </summary>
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

        /// <summary>
        /// Create database migration with specified name
        /// </summary>
        public void AddMigration(string migrationName)
        {
            migrationName = migrationName ?? "Init";
            _commandLineService.RunCommand($"dotnet ef migrations add --startup-project {_pathService.WebApiDirPath} --project {_pathService.CoreDirPath} {migrationName}");
        }

        /// <summary>
        /// Update database using specified migration or all migrations
        /// </summary>
        /// <param name="targetMigrationName">Empty target migration name applies all existing migrations</param>
        public void UpdateDatabase(string targetMigrationName = "")
        {
            _commandLineService.RunCommand($"dotnet ef database update --startup-project {_pathService.WebApiDirPath} --project {_pathService.CoreDirPath} {targetMigrationName}");
        }
    }
}
