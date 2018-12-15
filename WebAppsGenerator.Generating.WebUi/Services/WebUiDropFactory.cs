using System;
using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class WebUiDropFactory : BaseDropFactory
    {

        public WebUiDropFactory(IGeneratorConfiguration generatorConfiguration) : base(generatorConfiguration)
        {
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
            throw new NotImplementedException();
        }
    }
}
