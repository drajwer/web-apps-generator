using System.Collections.Generic;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Root implementation of <see cref="IGenerator"/>. Runs all received generators after fixing entities.
    /// </summary>
    public class RootGenerator : IGenerator
    {
        private readonly IEnumerable<IGenerator> _generators;
        private IEntitiesFixer _entitiesFixer;
        public RootGenerator(IEnumerable<IGenerator> generators, IEntitiesFixer entitiesFixer)
        {
            _generators = generators;
            _entitiesFixer = entitiesFixer;
        }

        public void Generate(IEnumerable<Entity> entities)
        {
            _entitiesFixer.FixEntities(entities);

            foreach (var generator in _generators)
            {
                generator.Generate(entities);
            }
        }
    }
}
