using System.Collections.Generic;
using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Tests.Mocks
{
    /// <summary>
    /// Stores file paths in a list
    /// </summary>
    public class FileServiceMock : IFileService
    {
        public List<string> CreatedFiles { get; set; }

        public FileServiceMock()
        {
            CreatedFiles = new List<string>();
        }

        public void CreateFromTemplate(FileInfo fileInfo, Drop templatingObject)
        {
            CreatedFiles.Add(fileInfo.OutputPath);
        }
    }
}
