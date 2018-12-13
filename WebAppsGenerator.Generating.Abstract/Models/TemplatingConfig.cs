using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Models
{
    public class TemplatingConfig
    {
        public FileInfo FileInfo { get; set; }
        public string DropId { get; set; }
        public bool Multiple { get; set; }
    }
}
