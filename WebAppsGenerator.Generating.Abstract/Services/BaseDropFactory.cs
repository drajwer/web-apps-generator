using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class BaseDropFactory : IDropFactory
    {
        private readonly IGeneratorConfiguration _generatorConfiguration;

        public BaseDropFactory(IGeneratorConfiguration generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
        }

        public virtual BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                case null:
                    return new BaseDrop(_generatorConfiguration);
                default:
                    throw new ArgumentOutOfRangeException($"Cannot create drop for {dropId}");

            }
        }

        public virtual List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities)
        {
            throw new ArgumentOutOfRangeException($"Cannot create drop for {dropId}");
        }
    }
}
