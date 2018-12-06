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
    }
}
