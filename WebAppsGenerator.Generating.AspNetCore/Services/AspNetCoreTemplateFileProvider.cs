using System.Reflection;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    /// <summary>
    /// <see cref="FileService"/> initialized using web api specific information
    /// </summary>
    public class AspNetCoreFileService : FileService, IAspNetCoreFileService
    {
        public AspNetCoreFileService(LiquidTemplateService templateService, IOverwriteService overwriteService) 
            : base(new TemplateFileProvider(Assembly.GetAssembly(typeof(AspNetCoreFileService))), templateService, overwriteService)
        {
        }
    }
}
