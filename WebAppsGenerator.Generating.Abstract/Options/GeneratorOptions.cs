using System.Collections.Generic;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    public class GeneratorOptions
    {
        public string ProjectName { get; set; }
        public string OutputPath { get; set; }
        public List<NuGetPackageDetails> CoreProjectPackages { get; set; }
    }
}
