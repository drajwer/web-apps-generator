using System;
using System.IO;
using System.Linq;

namespace WebAppsGenerator.Core.Files.Providers
{
    public class FlatDirectoryFilesProvider : IFilesProvider
    {
        private readonly string _dirName;
        private readonly string _extension;
        public FlatDirectoryFilesProvider(string dirName, string extension = null)
        {
            _dirName = dirName;
            _extension = extension;
        }
        public string[] GetFiles()
        {
            if (!Directory.Exists(_dirName))
                throw new ArgumentException($"Specified directory: {_dirName} does not exists");

            var files = Directory.GetFiles(_dirName);
            if (!string.IsNullOrEmpty(_extension))
                files = files.Where(f => f.ToLower().Contains($".{_extension.ToLower()}")).ToArray();

            return files.Select(Path.GetFullPath).ToArray();
        }
    }
}
