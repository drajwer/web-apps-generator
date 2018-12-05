using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Templating class to expose one entity's field only
    /// </summary>
    public class FieldDrop : Drop
    {
        public string Name { get; }
        public TypeDrop Type { get; set; }

        public FieldDrop(Field field)
        {
            Name = field.Name;
            Type = new TypeDrop(field.Type);
        }
    }
}
