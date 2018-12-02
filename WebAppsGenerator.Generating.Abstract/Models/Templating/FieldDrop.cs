using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class FieldDrop
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
