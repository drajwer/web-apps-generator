using System.Collections.Generic;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IGeneratorConfiguration
    {
        string ProjectName { get; }
        string OutputPath { get; }
        List<NuGetPackageDetails> CoreProjectPackages { get; set; }
    }
}
