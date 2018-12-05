using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides generating code based on given entities
    /// </summary>
    public interface IGenerator
    {
        void Generate(IEnumerable<Entity> entities);
    }
}
