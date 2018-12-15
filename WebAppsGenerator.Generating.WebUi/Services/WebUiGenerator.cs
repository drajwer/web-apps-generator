using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebUiGenerator : BaseGenerator
    {
        public WebUiGenerator(IGeneratorConfiguration generatorConfiguration, IWebUiFileService fileService,
            IWebUiProjectTemplatingConfigProvider templatingConfigProvider, WebUiDropFactory dropFactory)
            : base(generatorConfiguration, dropFactory, templatingConfigProvider, fileService)
        {
        }
    }
}
