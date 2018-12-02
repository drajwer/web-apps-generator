using System.IO;
using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.Abstract.Services
{
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

            using (var streamWriter = File.CreateText(CreatePath(fileInfo.OutputPath, fileName)))
            {
                streamWriter.Write(rendered);
            }
        }

        private string CreatePath(string outputPath, string fileName)
        {
            return Path.Combine(outputPath, fileName);
        }
    }
}
