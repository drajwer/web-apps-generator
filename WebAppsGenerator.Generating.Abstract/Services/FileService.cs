using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Standard implementation of <see cref="IFileService"/>
    /// </summary>
    public class FileService : IFileService
    {
        private readonly TemplateFileProvider _fileProvider;
        private readonly LiquidTemplateService _templateService;

        public FileService(TemplateFileProvider fileProvider, LiquidTemplateService templateService)
        {
            _fileProvider = fileProvider;
            _templateService = templateService;
        }

        public void CreateFromTemplate(FileInfo fileInfo, Drop templatingObject)
        {
            var template = _fileProvider.GetTemplate(fileInfo);
            var fileName = _templateService.RenderTemplate(fileInfo.NameTemplate, templatingObject);
            var rendered = _templateService.RenderTemplate(template, templatingObject);

            Directory.CreateDirectory(fileInfo.OutputPath);
            WriteFile(fileInfo, fileName, rendered);
        }

        private void WriteFile(FileInfo fileInfo, string fileName, string rendered)
        {
            using (var streamWriter = File.CreateText(CreatePath(fileInfo.OutputPath, fileName)))
            {
                streamWriter.Write(rendered);
            }
        }

        public void CreateFromTemplate(FileInfo fileInfo, List<Drop> templatingObjects)
        {
            var template = _fileProvider.GetTemplate(fileInfo);
            var fileNames = _templateService.RenderTemplate(fileInfo.NameTemplate, templatingObjects).ToArray();
            var renderedFiles = _templateService.RenderTemplate(template, templatingObjects).ToArray();

            Directory.CreateDirectory(fileInfo.OutputPath);
            for (int i = 0; i < fileNames.Length; i++)
            {
                WriteFile(fileInfo, fileNames[i], renderedFiles[i]);
            }
        }


        private string CreatePath(string outputPath, string fileName)
        {
            return Path.Combine(outputPath, fileName);
        }
    }
}
