using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class WebApiProjectGenerator : BaseGenerator
    {
        public WebApiProjectGenerator(IGeneratorConfiguration generatorConfiguration, IAspNetCoreFileService fileService,
            IWebApiProjectTemplatingConfigProvider templatingConfigProvider, CSharpDropFactory dropFactory)
            : base(generatorConfiguration, dropFactory, templatingConfigProvider, fileService)
        {
        }
    }
}
