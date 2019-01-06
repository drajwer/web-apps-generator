using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides validating given entities.
    /// </summary>
    public interface IValidator
    {
        void Validate(IEnumerable<Entity> entities);
    }
}
