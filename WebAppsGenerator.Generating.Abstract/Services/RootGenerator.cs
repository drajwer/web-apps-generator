using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Root implementation of <see cref="IGenerator"/>. Runs all received generators.
    /// </summary>
    public class RootGenerator : IGenerator
    {
        private readonly IEnumerable<IGenerator> _generators;

        public RootGenerator(IEnumerable<IGenerator> generators)
        {
            _generators = generators;
        }

        public void Generate(IEnumerable<Entity> entities)
        {
            foreach (var generator in _generators)
            {
                generator.Generate(entities);
            }
        }
    }
}
