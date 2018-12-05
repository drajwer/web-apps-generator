﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<FieldDrop> Fields { get; set; }
        public EntityDrop(Entity entity)
        {
            Name = entity.Name;
            PluralName = PluralizationHelper.Pluralize(Name);
            Fields = entity.Fields.Select(f => new FieldDrop(f)).ToList();
        }
    }
}
