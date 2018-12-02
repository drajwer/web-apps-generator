using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Models
{
    public class DirectoryInfo
    {
        public List<FileInfo> Files { get; set; }
        public List<DirectoryInfo> Directories { get; set; }
        public string NameTemplate { get; set; }
    }
}
