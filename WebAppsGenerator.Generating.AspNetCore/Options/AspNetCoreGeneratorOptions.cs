using System.Collections.Generic;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Generating.AspNetCore.Options
{
    /// <summary>
    /// Helper class used for injecting configuration to generator
    /// </summary>
    public class AspNetCoreGeneratorOptions
    {
        public List<NuGetPackageDetails> CoreProjectPackages { get; set; }
    }
}
