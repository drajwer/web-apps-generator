using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class Entity
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
        public List<Annotation> Annotations { get; set; }
    }
}
