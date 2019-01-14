using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class AnnotationDefinition
    {
        public string Name { get; set; }
        public List<AnnotationParamDefinition> Params { get; set; }
        public bool IsClassAnnotation { get; set; }
        public bool AllowNoParams { get; set; }
        /// <summary>
        /// Specifies types of properties to which an annotation can be used
        /// </summary>
        public List<TypeKind> AllowedPropTypeKinds { get; set; }
    }
}