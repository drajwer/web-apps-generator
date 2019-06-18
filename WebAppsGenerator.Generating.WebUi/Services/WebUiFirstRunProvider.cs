using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebUiFirstRunProvider : IWebUiFirstRunProvider
    {
        private readonly SolutionPathService _pathService;

        public WebUiFirstRunProvider(SolutionPathService pathService)
        {
            _pathService = pathService;
        }

        public bool IsFirstRun()
        {
            var webApiCsProjPath = Path.Combine(_pathService.WebProjectDirPath, "package.json");
            return !File.Exists(webApiCsProjPath);
        }
    }
}
