using System;
using System.IO;
using System.Linq;

namespace WebAppsGenerator.Core.Files.Providers
{
    public class FlatDirectoryFilesProvider : BaseFilesProvider
    {
        public FlatDirectoryFilesProvider(string dirName, string extension = null) : base(dirName, extension)
        {
        }

        public override string[] GetFiles()
        {
            if (!Directory.Exists(DirName))
                throw new ArgumentException($"Specified directory: {DirName} does not exists");

            var files = Directory.GetFiles(DirName);
            if (!string.IsNullOrEmpty(Extension))
                files = files.Where(f => f.ToLower().Contains($".{Extension.ToLower()}")).ToArray();

            return files.Select(Path.GetFullPath).ToArray();
        }
    }
}
