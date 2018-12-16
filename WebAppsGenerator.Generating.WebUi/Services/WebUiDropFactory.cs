using System;
using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebUiDropFactory : BaseDropFactory
    {
        private readonly IGeneratorConfiguration _generatorConfiguration;
        private readonly SolutionPathService _pathService;

        public WebUiDropFactory(IGeneratorConfiguration generatorConfiguration, SolutionPathService pathService) : base(generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
            _pathService = pathService;
        }

        public override BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                default:
                    return base.CreateDrop(dropId, entities);
            }

        }

        public override List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                case "EntityDrop":
                    return new List<BaseDrop>(entities.Select(e => new SingleEntityDrop(_generatorConfiguration, _pathService, e)).ToList());
                default:
                    throw new ArgumentOutOfRangeException($"Cannot create drop for {dropId}");

            }
        }
    }
}
