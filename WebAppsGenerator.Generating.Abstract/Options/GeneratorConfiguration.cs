using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Default implementation of <see cref="IGeneratorConfiguration"/>
    /// </summary>
    public class GeneratorConfiguration : IGeneratorConfiguration
    {
        public string ProjectName { get; set; }
        public string OutputPath { get; set; }
        public string MigrationName { get; set; }
        public List<NuGetPackageDetails> CoreProjectPackages { get; set; }

        public GeneratorConfiguration(IOptions<GeneratorOptions> generatorOptions)
        {
            var options = generatorOptions.Value;
            ProjectName = options.ProjectName;
            OutputPath = options.OutputPath;
            MigrationName = options.MigrationName;
            CoreProjectPackages = options.CoreProjectPackages;
        }
    }
}
