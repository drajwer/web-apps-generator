using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IDropFactory
    {
        BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities);
        List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities);
    }
}