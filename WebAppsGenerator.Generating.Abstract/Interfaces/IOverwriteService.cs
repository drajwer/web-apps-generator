using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides methods for handling file overwriting
    /// </summary>
    public interface IOverwriteService
    {
        /// <summary>
        /// Returns true if the file should be overwritten
        /// </summary>
        /// <param name="filePath">File to check</param>
        /// <param name="defaultValue">Value to use if service cannot determine return value</param>
        bool ShouldOverwriteFile(string filePath, bool defaultValue);

        /// <summary>
        /// Returns dictionary which reflects project file structure.
        /// Values are flags if file with given key should be overwritten in the next run
        /// </summary>
        Dictionary<string, bool> GetOverwritesDictionary();

        /// <summary>
        /// Set OverWriteAll flag to true.
        /// </summary>
        void SetOverwriteAll();

        /// <summary>
        /// Reset OverWriteAll flag to its initial value.
        /// </summary
        void ResetOverwriteAll();
    }
}
