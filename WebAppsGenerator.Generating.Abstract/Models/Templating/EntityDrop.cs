using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class EntityDrop
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
