using System.Collections.Generic;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides templating configuration
    /// </summary>
    public interface ITemplatingConfigProvider
    {
        /// <summary>
        /// Provides templating configuration for given section
        /// </summary>
        TemplatingConfig GetConfig(string sectionName);

        /// <summary>
        /// Provides templating configuration for all templates
        /// </summary>
        IEnumerable<TemplatingConfig> GetTemplatingConfigs();
    }
}