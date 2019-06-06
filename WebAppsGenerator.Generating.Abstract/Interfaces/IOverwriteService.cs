using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IOverwriteService
    {
        bool ShouldOverwriteFile(string filePath, bool defaultValue);
        Dictionary<string, bool> GetOverwritesDictionary();
        void SetOverwriteAll();
        void ResetOverwriteAll();
    }
}
