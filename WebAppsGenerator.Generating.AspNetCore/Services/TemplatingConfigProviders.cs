using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    // Markers

    /// <summary>
    /// Marker for Core project templating config provider.
    /// </summary>
    public interface ICoreProjectTemplatingConfigProvider : ITemplatingConfigProvider
    {
    }

    /// <summary>
    /// Marker for Web Api project templating config provider.
    /// </summary>
    public interface IWebApiProjectTemplatingConfigProvider : ITemplatingConfigProvider
    {
    }

    // Implementations

    /// <summary>
    /// <see cref="TemplatingConfigProvider"/> wrapped with combining paths with Core project base path.
    /// </summary>
    public class CoreProjectTemplatingConfigProvider : TemplatingConfigProvider, ICoreProjectTemplatingConfigProvider
    {
        private readonly SolutionPathService _pathService;
        public CoreProjectTemplatingConfigProvider(SolutionPathService pathService) : base(typeof(CoreProjectTemplatingConfigProvider).Assembly, "Core")
        {
            _pathService = pathService;
        }

        protected override TemplatingConfig ReadConfig(string sectionName)
        {
            var config = base.ReadConfig(sectionName);
            if (config?.FileInfo != null)
            {
                config.FileInfo.OutputPath = config.FileInfo.OutputPath.Split('/')
                    .Aggregate(_pathService.WebApiDirPath, Path.Combine);
            }
            return config;
        }

        public override IEnumerable<TemplatingConfig> GetTemplatingConfigs()
        {
            foreach (var templatingConfig in base.GetTemplatingConfigs())
            {
                templatingConfig.FileInfo.OutputPath =
                    Path.Combine(_pathService.CoreDirPath, templatingConfig.FileInfo.OutputPath);

                yield return templatingConfig;
            }
        }
    }

    /// <summary>
    /// <see cref="TemplatingConfigProvider"/> wrapped with combining paths with Web Api project base path.
    /// </summary>
    public class WebApiProjectTemplatingConfigProvider : TemplatingConfigProvider, IWebApiProjectTemplatingConfigProvider
    {
        private readonly SolutionPathService _pathService;
        public WebApiProjectTemplatingConfigProvider(SolutionPathService pathService) : base(typeof(WebApiProjectTemplatingConfigProvider).Assembly, "WebApi")
        {
            _pathService = pathService;
        }

        protected override TemplatingConfig ReadConfig(string sectionName)
        {
            var config = base.ReadConfig(sectionName);
            if (config?.FileInfo != null)
            {
                config.FileInfo.OutputPath = config.FileInfo.OutputPath.Split('/')
                    .Aggregate(_pathService.WebApiDirPath, Path.Combine);
            }
            return config;
        }

        public override IEnumerable<TemplatingConfig> GetTemplatingConfigs()
        {
            foreach (var templatingConfig in base.GetTemplatingConfigs())
            {
                templatingConfig.FileInfo.OutputPath =
                    Path.Combine(_pathService.WebApiDirPath, templatingConfig.FileInfo.OutputPath);

                yield return templatingConfig;
            }
        }
    }
}
