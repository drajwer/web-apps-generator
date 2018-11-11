using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Interfaces
{
    public interface IGenerator
    {
        void ProcessVisitorResults(IEnumerable<Entity> entities);
    }
}
