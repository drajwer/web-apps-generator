using System.Collections.Generic;
using DotLiquid;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generator.Models
{
    public class FieldDrop : Drop
    {
        private readonly Field _field;

        public List<Annotation> Annotations => _field.Annotations;
        public string Name => _field.Name;

        public FieldDrop(Field field)
        {
            _field = field;
        }
    }
}