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
        private const char PathSeparator = '/';
        private readonly TemplateFileProvider _fileProvider;
        private readonly LiquidTemplateService _templateService;
        private readonly IOverwriteService _overwriteService;
        public FileService(TemplateFileProvider fileProvider, LiquidTemplateService templateService, IOverwriteService overwriteService)
        {
            _fileProvider = fileProvider;
            _templateService = templateService;
            _overwriteService = overwriteService;
        }

        public void CreateFromTemplate(FileInfo fileInfo, Drop templatingObject)
        {
            var template = _fileProvider.GetTemplate(fileInfo);
            var filePath = _templateService.RenderTemplate(fileInfo.NameTemplate, templatingObject);
            var rendered = _templateService.RenderTemplate(template, templatingObject);
            var pathSplitted = filePath.Split(PathSeparator).ToList();
            var dirPath = pathSplitted.SkipLast(1).Aggregate(fileInfo.OutputPath, Path.Combine);
            var fileName = pathSplitted.Last();
            Directory.CreateDirectory(dirPath);

            WriteFile(dirPath, fileName, rendered, fileInfo.Overwrite);
        }


        public void CreateFromTemplate(FileInfo fileInfo, List<Drop> templatingObjects)
        {
            var template = _fileProvider.GetTemplate(fileInfo);
            var fileNames = _templateService.RenderTemplate(fileInfo.NameTemplate, templatingObjects).ToArray();
            var renderedFiles = _templateService.RenderTemplate(template, templatingObjects).ToArray();

            Directory.CreateDirectory(fileInfo.OutputPath);
            for (int i = 0; i < fileNames.Length; i++)
            {
                WriteFile(fileInfo.OutputPath, fileNames[i], renderedFiles[i], fileInfo.Overwrite);
            }
        }

        private void WriteFile(string dirPath, string fileName, string content, bool overwriteDefault)
        {
            var filePath = CreatePath(dirPath, fileName);
            var overwrite = _overwriteService.ShouldOverwriteFile(filePath, overwriteDefault);
            if (File.Exists(filePath) && !overwrite)
                return;
            WriteFile(filePath, content);
        }
        private void WriteFile(string filePath, string rendered)
        {
            using (var streamWriter = File.CreateText(filePath))
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
