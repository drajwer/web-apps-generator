using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Provides templating configuration for web UI templates
    /// </summary>
    public class WebUiTemplateConfigProvider : TemplatingConfigProvider, IWebUiProjectTemplatingConfigProvider
    {
        private readonly SolutionPathService _pathService;
        public WebUiTemplateConfigProvider(SolutionPathService pathService)
            : base(typeof(WebUiTemplateConfigProvider).Assembly, "WebUI")
        {
            _pathService = pathService;
        }

        protected override TemplatingConfig ReadConfig(string sectionName)
        {
            var config = base.ReadConfig(sectionName);
            if (config?.FileInfo != null)
            {
                config.FileInfo.OutputPath = config.FileInfo.OutputPath.Split('/')
                    .Aggregate(_pathService.WebProjectDirPath, Path.Combine);
            }
            return config;
        }

        public override IEnumerable<TemplatingConfig> GetTemplatingConfigs()
        {
            foreach (var templatingConfig in base.GetTemplatingConfigs())
            {
                templatingConfig.FileInfo.OutputPath =
                    Path.Combine(_pathService.WebProjectDirPath, templatingConfig.FileInfo.OutputPath);

                yield return templatingConfig;
            }
        }
    }
}
