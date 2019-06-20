using System.Collections.Generic;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Tests.Mocks
{
    public class OverwriteServiceMock : IOverwriteService
    {
        public bool? OverwriteFile { get; set; }
        public Dictionary<string, bool> OverwritesDictionary { get; set; }
        
        public bool ShouldOverwriteFile(string filePath, bool defaultValue) => OverwriteFile ?? defaultValue;

        public Dictionary<string, bool> GetOverwritesDictionary() => OverwritesDictionary;

        public void SetOverwriteAll()
        {
        }

        public void ResetOverwriteAll()
        {
        }
    }
}
