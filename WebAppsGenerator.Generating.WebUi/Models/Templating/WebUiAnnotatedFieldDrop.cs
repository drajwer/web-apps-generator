using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.WebUi.Extensions;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends FieldDrop by adding properties that store information extracted from annotations
    /// </summary>
    public class WebUiAnnotatedFieldDrop : AnnotatedFieldDrop
    {
        public string DisplayName { get; set; }

        public WebUiAnnotatedFieldDrop(Field field) : base(field)
        {
            // assign default values to helper properties in case they are not filled later
            DisplayName = Name;

            this.ParseFieldAnnotations(field.Annotations);
        }
    }
}
