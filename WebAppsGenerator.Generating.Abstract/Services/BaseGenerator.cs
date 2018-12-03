using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class BaseGenerator: IGenerator
    {
        protected IGeneratorConfiguration GeneratorConfiguration;

        public BaseGenerator(IGeneratorConfiguration generatorConfiguration)
        {
            GeneratorConfiguration = generatorConfiguration;
        }

        public virtual void Generate(IEnumerable<Entity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
