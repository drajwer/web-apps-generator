using System.IO;
using System.Linq;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public interface ICoreProjectTemplatingConfigProvider : ITemplatingConfigProvider
    {
    }
    public interface IWebApiProjectTemplatingConfigProvider : ITemplatingConfigProvider
    {
    }

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
            config.FileInfo.OutputPath = config.FileInfo.OutputPath.Split('/')
                .Aggregate(_pathService.CoreDirPath, Path.Combine);
            return config;
        }
    }

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
            config.FileInfo.OutputPath = config.FileInfo.OutputPath.Split('/')
                .Aggregate(_pathService.CoreDirPath, Path.Combine);
            return config;
        }
    }
}
