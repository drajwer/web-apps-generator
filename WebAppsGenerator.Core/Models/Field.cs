using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class Field
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public List<Annotation> Annotations { get; set; }
    }
}
