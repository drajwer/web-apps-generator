using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Templating class containing all entities
    /// </summary>
    public class DefaultDrop : BaseDrop
    {
        public List<Entity> Entities { get; }
        public DefaultDrop(IGeneratorConfiguration generatorConfiguration, List<Entity> entities) : base(generatorConfiguration)
        {
            Entities = entities;
        }
    }
}
