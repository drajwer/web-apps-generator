using System.Reflection;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    /// <summary>
    /// File service implementation for web UI generator
    /// </summary>
    public class WebUiFileService : FileService, IWebUiFileService
    {
        public WebUiFileService(LiquidTemplateService templateService, IOverwriteService overwriteService)
            : base(new TemplateFileProvider(Assembly.GetAssembly(typeof(WebUiFileService))), templateService, overwriteService)
        {
        }
    }
}
