using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.AspNetCore.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class SingleEntityDrop : WebApiBaseDrop
    {
        public SingleEntityDrop(IGeneratorConfiguration generatorConfiguration, SolutionPathService solutionPathService, Entity entity) 
            : base(solutionPathService, generatorConfiguration)
        {
            Entity = new WebApiAnnotatedEntityDrop(entity);
        }

        public SingleEntityDrop(IGeneratorConfiguration generatorConfiguration, SolutionPathService pathService, EntityDrop entity)
            : base(pathService, generatorConfiguration)
        {
            Entity = entity;
        }

        public EntityDrop Entity { get; }
    }
}
