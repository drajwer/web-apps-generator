using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.WebUi.Extensions;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends EntityDrop by adding properties that store information extracted from annotations
    /// </summary>
    public class AnnotatedEntityDrop : EntityDrop
    {
        public string DisplayName { get; set; }

        public AnnotatedEntityDrop(Entity entity) : base(entity)
        {
            Fields = entity.Fields.Select(f => new AnnotatedFieldDrop(f)).OfType<FieldDrop>().ToList();
            IdField = Fields.FirstOrDefault(f => f.Name == "Id");

            // assign default values to helper properties in case they are not filled later
            DisplayName = Name;

            this.ParseEntityAnnotations(entity.Annotations);
        }

    }
}
