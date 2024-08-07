﻿using System.IO;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    /// <summary>
    /// Provides information about first run based on csproj file existence
    /// </summary>
    public class AspNetCoreFirstRunProvider : IAspNetCoreFirstRunProvider
    {
        private readonly SolutionPathService _pathService;

        public AspNetCoreFirstRunProvider(SolutionPathService pathService)
        {
            _pathService = pathService;
        }

        public bool IsFirstRun()
        {
            var webApiCsProjPath = Path.Combine(_pathService.WebApiDirPath, $"{_pathService.WebApiProjectName}.csproj");
            return !File.Exists(webApiCsProjPath);
        }
    }
}
