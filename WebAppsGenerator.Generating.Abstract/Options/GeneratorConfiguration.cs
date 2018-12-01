using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    public class GeneratorConfiguration : IGeneratorConfiguration
    {
        public string ProjectName { get; set; }
        public string OutputPath { get; set; }
    }
}
