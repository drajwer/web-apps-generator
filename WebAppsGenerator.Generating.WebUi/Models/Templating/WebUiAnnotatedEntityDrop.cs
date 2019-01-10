using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Extensions;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends EntityDrop by adding properties that store information 
    /// extracted from annotations specific to WebUI generator
    /// </summary>
    public class WebUiAnnotatedEntityDrop : AnnotatedEntityDrop
    {
        public string DisplayName { get; set; }
        public string PluralDisplayName { get; set; }

        public bool HideInMenu { get; set; }

        public WebUiAnnotatedEntityDrop(Entity entity) : base(entity)
        {
            Fields = entity.Fields.Select(f => new WebUiAnnotatedFieldDrop(f)).OfType<FieldDrop>().ToList();
            IdField = Fields.FirstOrDefault(f => f.Name == "Id");

            // assign default values to helper properties in case they are not filled later
            DisplayName = Name;
            PluralDisplayName = PluralName;

            this.ParseEntityAnnotations(entity.Annotations);
        }

    }
}
