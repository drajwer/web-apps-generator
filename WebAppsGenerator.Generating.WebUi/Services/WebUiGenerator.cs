using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    /// <summary>
    /// Generates all files for web UI project
    /// </summary>
    public class WebUiGenerator : BaseGenerator, IWebUiChildGenerator
    {
        public WebUiGenerator(IGeneratorConfiguration generatorConfiguration, IWebUiFileService fileService,
            IWebUiProjectTemplatingConfigProvider templatingConfigProvider, WebUiDropFactory dropFactory)
            : base(generatorConfiguration, dropFactory, templatingConfigProvider, fileService)
        {
        }
    }
}
