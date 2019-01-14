using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.AspNetCore.Extensions;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends AnnotatedFieldDrop by adding properties that store information
    /// extracted from annotations specific to AspNetCore generator
    /// </summary>
    public class WebApiAnnotatedFieldDrop : AnnotatedFieldDrop
    {
        public string InverseProperty { get; set; }

        public WebApiAnnotatedFieldDrop(Field field) : base(field)
        {
            this.ParseFieldAnnotations(field.Annotations);
        }
    }
}
