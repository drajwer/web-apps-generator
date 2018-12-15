using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class CSharpDropFactory : BaseDropFactory
    {
        private readonly SolutionPathService _pathService;
        private readonly IGeneratorConfiguration _generatorConfiguration;
        private readonly ModelService _modelService;

        public CSharpDropFactory(SolutionPathService pathService, IGeneratorConfiguration generatorConfiguration, 
            ModelService modelService) : base(generatorConfiguration)
        {
            _pathService = pathService;
            _generatorConfiguration = generatorConfiguration;
            _modelService = modelService;
        }

        public override BaseDrop CreateDrop(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                case "WebApiBase":
                    return new WebApiBaseDrop(_pathService, _generatorConfiguration);
                case "EntityList":
                    return new EntityListDrop(_generatorConfiguration, _pathService, entities);
                case "ModelList":
                    return new EntityListDrop(_generatorConfiguration, _pathService, _modelService.CreateModelDrops(entities));
                case "ModelListNoJoins":
                    return new EntityListDrop(_generatorConfiguration, _pathService,
                        _modelService.CreateModelDrops(entities).Where(d => !d.IsJoinModel));
                default:
                    return base.CreateDrop(dropId, entities);
            }
        }

        public override List<BaseDrop> CreateDropList(string dropId, IEnumerable<Entity> entities)
        {
            switch (dropId)
            {
                case "ModelDrops":
                    return GetModelDrops(entities).OfType<BaseDrop>().ToList();

                case "ModelDropsNoJoins":
                    return GetModelDrops(entities).Where(d => (d.Entity is ModelDrop drop) && !drop.IsJoinModel)
                        .OfType<BaseDrop>().ToList();

                case "ModelDropsJoins":
                    return GetModelDrops(entities).Where(d => (d.Entity is ModelDrop drop) && drop.IsJoinModel)
                        .OfType<BaseDrop>().ToList();
                default:
                    throw new ArgumentOutOfRangeException($"Cannot create drop for {dropId}");
            }
        }

        private IEnumerable<SingleEntityDrop> GetModelDrops(IEnumerable<Entity> entities)
        {
            return _modelService.CreateModelDrops(entities)
                .Select(d => new SingleEntityDrop(_generatorConfiguration, _pathService, d));
        }
    }
}
