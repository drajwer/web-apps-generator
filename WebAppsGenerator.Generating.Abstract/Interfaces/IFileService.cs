using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides creating output files.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Generates file from given template and writes it to specified output location.
        /// </summary>
        /// <param name="fileInfo">Describes template and output location</param>
        /// <param name="templatingObject">Provides data needed by template</param>
        void CreateFromTemplate(FileInfo fileInfo, Drop templatingObject);
    }
}