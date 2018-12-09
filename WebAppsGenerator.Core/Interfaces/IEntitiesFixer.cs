using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Interfaces
{
    public interface IEntitiesFixer
    {
        void FixEntities(IEnumerable<Entity> entities);
    }
}