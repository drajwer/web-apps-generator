using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.WebUi.Services;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    public class SingleEntityDrop : WebUiBaseDrop
    {
        public SingleEntityDrop(IGeneratorConfiguration generatorConfiguration, SolutionPathService solutionPathService, Entity entity)
            : base(solutionPathService, generatorConfiguration)
        {
            Entity = new EnhancedEntityDrop(entity);
        }

        public SingleEntityDrop(IGeneratorConfiguration generatorConfiguration, SolutionPathService pathService, EnhancedEntityDrop entity)
            : base(pathService, generatorConfiguration)
        {
            Entity = entity;
        }

        public EnhancedEntityDrop Entity { get; }
    }
}