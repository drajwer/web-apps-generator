using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Interfaces
{
    public interface IEntitiesFixer
    {
        /// <summary>
        /// Prepares entities for further processing by Id property creation and defining relations between entities
        /// </summary>
        void FixEntities(IEnumerable<Entity> entities);
    }
}