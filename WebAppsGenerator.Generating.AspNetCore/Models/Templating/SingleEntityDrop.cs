using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    class SingleEntityDrop : BasicDrop
    {
        public SingleEntityDrop(IGeneratorConfiguration generatorConfiguration, Entity entity) : base(generatorConfiguration)
        {
            Entity = new EntityDrop(entity);
        }

        public EntityDrop Entity { get; }
    }
}
