using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
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

        public string DisplayInDropdownField { get; set; }

        public WebUiAnnotatedEntityDrop(Entity entity) : base(entity)
        {
            var annotatedFields = entity.Fields.Select(f => new WebUiAnnotatedFieldDrop(f)).ToList();
            var displayInDropdownField = annotatedFields.FirstOrDefault(f => f.DisplayInDropdown);
            var idField = annotatedFields.FirstOrDefault(f => f.Name == "Id");
            if (idField != null)
                idField.DisplayInList = true;

            Fields = annotatedFields.OfType<FieldDrop>().ToList();
            IdField = idField;

            // assign default values to helper properties in case they are not filled later
            DisplayName = Name;
            PluralDisplayName = PluralName;
            DisplayInDropdownField = displayInDropdownField?.Name ?? IdField?.Name;

            this.ParseEntityAnnotations(entity.Annotations);
        }

    }
}
