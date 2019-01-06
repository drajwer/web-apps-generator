using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebAppsGenerator.Core.Files.Providers
{
    public class DeepDirectoryFilesProvider : BaseFilesProvider
    {
        public DeepDirectoryFilesProvider(string dirName, string extension = null) : base(dirName, extension)
        {
        }

        public override string[] GetFiles()
        {
            return ScanDirectory(DirName).ToArray();
        }

        private IEnumerable<string> ScanDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                throw new ArgumentException($"Specified directory: {dirPath} does not exists");

            var files = GetFilesFromDirectory(dirPath); // files from currently scanned directory

            var subDirectories = Directory.EnumerateDirectories(dirPath);
            foreach (var subDirectory in subDirectories)
            {
                files = files.Concat(ScanDirectory(subDirectory)); // add files from every subdirectory
            }

            return files;
        }

        private IEnumerable<string> GetFilesFromDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                throw new ArgumentException($"Specified directory: {dirPath} does not exists");

            var files = Directory.GetFiles(dirPath);
            if (!string.IsNullOrEmpty(Extension))
                files = files.Where(f => f.ToLower().Contains($".{Extension.ToLower()}")).ToArray();

            return files.Select(Path.GetFullPath).ToArray();
        }
    }
}
