using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class AnnotationDefinition: Annotation
    {
        /// <summary>
        /// Specifies types of properties to which an annotation can be used
        /// </summary>
        public List<TypeKind> AllowedPropTypeKinds { get; set; }
    }
}