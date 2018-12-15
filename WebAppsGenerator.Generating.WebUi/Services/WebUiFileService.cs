using System.Reflection;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebUiFileService : FileService, IWebUiFileService
    {
        public WebUiFileService(LiquidTemplateService templateService)
            : base(new TemplateFileProvider(Assembly.GetAssembly(typeof(WebUiFileService))), templateService)
        {
        }
    }
}
