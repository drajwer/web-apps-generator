using System.Collections.Generic;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generator.Generator
{
    public class BaseGenerator: IGenerator
    {
        protected IGeneratorConfiguration _generatorConfiguration;

        public BaseGenerator(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }

        public virtual void Generate(IEnumerable<Entity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
