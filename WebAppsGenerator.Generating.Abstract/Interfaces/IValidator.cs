using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IValidator
    {
        void ValidateEntities(IEnumerable<Entity> entities);
        void ValidateTypes(IEnumerable<Entity> entities);
        void ValidateAnnotations(IEnumerable<Entity> entities);
    }
}
