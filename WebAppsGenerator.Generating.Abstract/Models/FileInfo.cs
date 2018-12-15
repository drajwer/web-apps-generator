﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Models
{
    public class FileInfo
    {
        public FileInfo(string nameTemplate, string templatePath, string outputPath)
        {
            NameTemplate = nameTemplate;
            TemplatePath = templatePath;
            OutputPath = outputPath;
        }

        public FileInfo()
        {
        }
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
