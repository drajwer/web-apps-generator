using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.AspNetCore.Extensions;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends AnnotatedEntityDrop by adding properties that store information 
    /// extracted from annotations specific to AspNetCore generator
    /// </summary>
    public class WebApiAnnotatedEntityDrop : AnnotatedEntityDrop
    {
        public string DisplayName { get; set; }

        public WebApiAnnotatedEntityDrop(Entity entity) : base(entity)
        {
            Fields = entity.Fields.Select(f => new WebApiAnnotatedFieldDrop(f)).OfType<FieldDrop>().ToList();
            IdField = Fields.FirstOrDefault(f => f.Name == "Id");

            // assign default values to helper properties in case they are not filled later
            DisplayName = Name;

            this.ParseEntityAnnotations(entity.Annotations);
        }

    }
}
