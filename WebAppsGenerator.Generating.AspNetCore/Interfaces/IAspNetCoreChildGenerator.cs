using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Interfaces
{
    /// <summary>
    /// Marker for Web Api generators which aren't root generator.
    /// </summary>
    public interface IAspNetCoreChildGenerator : IGenerator
    {
    }
}