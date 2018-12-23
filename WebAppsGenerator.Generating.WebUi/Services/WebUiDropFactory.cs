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
        private readonly TypeScriptEntityService _entityService;

        public WebUiDropFactory(IGeneratorConfiguration generatorConfiguration, SolutionPathService pathService, TypeScriptEntityService entityService) : base(generatorConfiguration)
        {
            _generatorConfiguration = generatorConfiguration;
            _pathService = pathService;
            _entityService = entityService;
        }

        public override BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                case "EntityList":
                    return new EntityListDrop(_generatorConfiguration, _pathService, _entityService.GetDrops(entities));
                case "WebUiBaseDrop":
                    return new WebUiBaseDrop(_pathService, _generatorConfiguration);
                default:
                    return base.CreateDrop(dropId, entities);
            }

        }

        public override List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                case "EntityDrop":
                    return new List<BaseDrop>(_entityService.GetDrops(entities).Select(e => new SingleEntityDrop(_generatorConfiguration, _pathService, e)).ToList());
                default:
                    throw new ArgumentOutOfRangeException($"Cannot create drop for {dropId}");

            }
        }
    }
}
