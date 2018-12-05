using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Models
{
    /// <summary>
    /// Contains information for generating directories
    /// </summary>
    public class DirectoryInfo
    {
        /// <summary>
        /// List of file templates to be generated inside this directory
        /// </summary>
        public List<FileInfo> Files { get; set; }

        /// <summary>
        /// List of directories to be generated inside this directory
        /// </summary>
        public List<DirectoryInfo> Directories { get; set; }

        /// <summary>
        /// Template used to generate directory name
        /// </summary>
        public string NameTemplate { get; set; }
    }
}
