using System.Collections.Generic;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Helper class used for injecting configuration to generator
    /// </summary>
    public class GeneratorOptions
    {
        public string ProjectName { get; set; }
        public string OutputPath { get; set; }
        public string MigrationName { get; set; }
        public List<NuGetPackageDetails> CoreProjectPackages { get; set; }
    }
}
