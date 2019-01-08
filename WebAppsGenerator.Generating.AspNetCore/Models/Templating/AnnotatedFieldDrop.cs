using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.AspNetCore.Extensions;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends FieldDrop by adding properties that store information extracted from annotations
    /// </summary>
    public class AnnotatedFieldDrop : FieldDrop
    {
        public string DisplayName { get; set; }

        public AnnotatedFieldDrop(Field field) : base(field)
        {
            this.ParseFieldAnnotations(field.Annotations);
        }
    }
}
