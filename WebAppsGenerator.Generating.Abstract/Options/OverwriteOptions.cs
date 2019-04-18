using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    public class OverwriteOptions
    {
        private Dictionary<string, bool> _filesToOverwrite { get; set; }
        private bool _overwriteAll { get; set; }

        public OverwriteOptions(Dictionary<string, bool> filesToOverwrite, bool overwriteAll)
        {
            _filesToOverwrite = filesToOverwrite;
            _overwriteAll = overwriteAll;
        }

        public bool ShouldOverwriteFile(string filePath)
        {
            if (!_filesToOverwrite.ContainsKey(filePath))
                return false;
            return _filesToOverwrite[filePath];
        }
    }
}
