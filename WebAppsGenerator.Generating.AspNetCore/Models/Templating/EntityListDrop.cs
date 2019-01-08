using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.AspNetCore.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class EntityListDrop : WebApiBaseDrop
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
            Entities = entities.Select(e => new AnnotatedEntityDrop(e)).OfType<EntityDrop>().ToList();
        }

        public List<EntityDrop> Entities { get; }
    }
}