using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Tests.Mocks
{
    public class DropFactoryMock : IDropFactory
    {
        public BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities)
        {
            return new BaseDrop(new GeneratorConfigurationMock());
        }

        public List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities)
        {
            var genConfiguration = new GeneratorConfigurationMock();

            return entities.Select(e => new BaseDrop(genConfiguration)).ToList();
        }
    }
}
