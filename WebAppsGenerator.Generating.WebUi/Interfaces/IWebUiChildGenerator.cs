using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.WebUi.Interfaces
{
    /// <summary>
    /// Marker for Web UI generators which aren't root generator.
    /// </summary>
    public interface IWebUiChildGenerator : IGenerator
    {
    }
}