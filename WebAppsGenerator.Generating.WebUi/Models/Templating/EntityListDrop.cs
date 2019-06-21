using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.WebUi.Services;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    /// <summary>
    /// Drop containing all provided entities
    /// </summary>
    public class EntityListDrop : WebUiBaseDrop
    {
        public EntityListDrop(IGeneratorConfiguration generatorConfiguration, SolutionPathService solutionPathService,
            IEnumerable<EntityDrop> entities) : base(solutionPathService, generatorConfiguration)
        {
            Entities = entities.ToList();
        }

        public EntityListDrop(IGeneratorConfiguration generatorConfiguration, SolutionPathService solutionPathService,
            IEnumerable<Entity> entities)
            : base(solutionPathService, generatorConfiguration)
        {
            Entities = entities.Select(e => new WebUiAnnotatedEntityDrop(e)).OfType<EntityDrop>().ToList();
        }

        public List<EntityDrop> Entities { get; }
    }
}