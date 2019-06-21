namespace WebAppsGenerator.Core.Models
{
    /// <summary>
    /// Definition of annotation parameter from configuration file
    /// </summary>
    public class AnnotationParamDefinition
    {
        public string Name { get; set; }
        public TypeKind Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
