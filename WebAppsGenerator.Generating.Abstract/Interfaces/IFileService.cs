using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IFileService
    {
        void CreateFromTemplate(FileInfo fileInfo, Drop templatingObject);
    }
}