using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Tests.Mocks
{
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
