using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class BasicDrop : Drop
    {
        private readonly IGeneratorConfiguration _generatorConfiguration;

        public BasicDrop(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }

        public string ProjectName => _generatorConfiguration.ProjectName;
        public string OutputPath => _generatorConfiguration.OutputPath;
    }
}
