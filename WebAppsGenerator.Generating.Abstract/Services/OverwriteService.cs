using System.Collections.Generic;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Default implementation of <see cref="IOverwriteService"/>
    /// It uses overwrite dictionary from json file.
    /// It also saves new overwrite values in other dictionary
    /// </summary>
    public class OverwriteService : IOverwriteService
    {    
        private readonly Dictionary<string, bool> _oldFilesToOverwrite;
        private readonly Dictionary<string, bool> _newFilesToOverwrite;

        private bool _overwriteAll;
        private bool _oldOverwriteAll;
    
        public OverwriteService(Dictionary<string, bool> filesToOverwrite, bool overwriteAll)
        {
            _oldFilesToOverwrite = filesToOverwrite ?? new Dictionary<string, bool>();
            _newFilesToOverwrite = new Dictionary<string, bool>();
            _overwriteAll = overwriteAll;
            _oldOverwriteAll = overwriteAll;
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

        public void SetOverwriteAll() => _overwriteAll = true;
        public void ResetOverwriteAll() => _overwriteAll = _oldOverwriteAll;

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
