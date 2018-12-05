using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Provides input data for template parser
    /// </summary>
    public class BaseDrop : Drop
    {
        private readonly IGeneratorConfiguration _generatorConfiguration;

        public BaseDrop(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }

        public string ProjectName => _generatorConfiguration.ProjectName;
        public string OutputPath => _generatorConfiguration.OutputPath;
    }
}
