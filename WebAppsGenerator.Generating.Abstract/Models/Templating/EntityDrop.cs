﻿using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Templating class to expose one entity only
    /// </summary>
    public class EntityDrop : Drop
    {
        public string Name { get; }
        public string PluralName { get; }

        public List<FieldDrop> Fields { get; protected set; }
        public FieldDrop IdField { get; protected set; }


        public EntityDrop(Entity entity)
        {
            Name = entity.Name;
            PluralName = PluralizationHelper.Pluralize(Name);
            Fields = entity.Fields.Select(f => new FieldDrop(f)).ToList();
            IdField = Fields.FirstOrDefault(f => f.Name == "Id"); 
        }
        
    }
}
