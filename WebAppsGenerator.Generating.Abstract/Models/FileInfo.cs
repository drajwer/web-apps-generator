using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Models
{
    public class FileInfo
    {
        /// <summary>
        /// Template used to generate file name
        /// </summary>
        public string NameTemplate { get; set; }

        /// <summary>
        /// Location of template used to generate file content
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// Location of output directory to put generated file on
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Default value specifying if the file should be overwritten
        /// </summary>
        public bool Overwrite { get; set; }

        public FileInfo(string nameTemplate, string templatePath, string outputPath, bool overwrite)
        {
            NameTemplate = nameTemplate;
            TemplatePath = templatePath;
            OutputPath = outputPath;
            Overwrite = overwrite;
        }

        public FileInfo()
        {
            
        }

        public FileInfo(FileInfo fileInfo)
        {
            NameTemplate = fileInfo.NameTemplate;
            TemplatePath = fileInfo.TemplatePath;
            OutputPath = fileInfo.OutputPath;
        }
    }
}
