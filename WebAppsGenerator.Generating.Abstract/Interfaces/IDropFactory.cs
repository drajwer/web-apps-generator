using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides Drop objects based on given id.
    /// </summary>
    public interface IDropFactory
    {
        /// <summary>
        /// Creates one Drop object related to provided entities
        /// </summary>
        /// <param name="dropId">Identifier of the drop</param>
        /// <param name="entities">Entities from given application model</param>
        /// <returns></returns>
        BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities);

        /// <summary>
        /// Creates Drop list with one Drop for every provided entity
        /// </summary>
        /// <param name="dropId">Identifier of the drop</param>
        /// <param name="entities">Entities from given application model</param>
        /// <returns></returns>
        List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities);
    }
}