using System.IO;
using System.Reflection;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Service for retrieving templates from embedded resources
    /// </summary>
    public class TemplateFileProvider : ITemplateFileProvider
    {
        private readonly Assembly _assembly;

        public TemplateFileProvider(Assembly assembly = null)
        {
            _assembly = assembly ?? Assembly.GetCallingAssembly();
        }
        public string GetTemplate(Models.FileInfo templateFileInfo)
        {
            var assemblyNamespace = _assembly.FullName.Split(',')[0];
            var resourceName = $"{assemblyNamespace}.Templates.{templateFileInfo.TemplatePath}";

            using (var reader = new StreamReader(_assembly.GetManifestResourceStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
