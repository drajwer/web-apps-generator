using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Models
{
    public class  TemplatingConfig
    {
        /// <summary>
        /// Detailed information about file to be generated from template
        /// </summary>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// Template for generated file name
        /// </summary>
        public string NameTemplate { get; set; }

        /// <summary>
        /// Identification of drop file to use when generating from template
        /// </summary>
        public string DropId { get; set; }

        /// <summary>
        /// Flag specifying if one file should be generated or one file per entity
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// Identification of drop file to use when generating from template
        /// </summary>
        public bool Overwrite { get; set; }
    }
}
