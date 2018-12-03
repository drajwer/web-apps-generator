using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IGenerator
    {
        void Generate(IEnumerable<Entity> entities);
    }
}
