using System.Collections.Generic;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    public class OverwriteService : IOverwriteService
    {
        private Dictionary<string, bool> _oldFilesToOverwrite { get; set; }
        private Dictionary<string, bool> _newFilesToOverwrite { get; set; }

        private bool _overwriteAll { get; set; }

        public OverwriteService(Dictionary<string, bool> filesToOverwrite, bool overwriteAll)
        {
            _oldFilesToOverwrite = filesToOverwrite ?? new Dictionary<string, bool>();
            _newFilesToOverwrite = new Dictionary<string, bool>();
            _overwriteAll = overwriteAll;
        }

        public bool ShouldOverwriteFile(string filePath, bool defaultValue)
        {
            var value = ShouldOverwriteFileInternal(filePath, defaultValue);
            _newFilesToOverwrite[filePath] = value;

            return value;
        }

        public Dictionary<string, bool> GetOverwritesDictionary()
        {
            if (_overwriteAll)
                return _oldFilesToOverwrite;
            return _newFilesToOverwrite;
        }

        private bool ShouldOverwriteFileInternal(string filePath, bool defaultValue)
        {
            if (_overwriteAll)
                return true;
            if (!_oldFilesToOverwrite.ContainsKey(filePath))
                return defaultValue;
            return _oldFilesToOverwrite[filePath];
        }
    }
}
