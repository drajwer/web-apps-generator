using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class Annotation
    {
        public string Name { get; set; }
        public List<AnnotationParam> Params { get; set; }
    }
}
