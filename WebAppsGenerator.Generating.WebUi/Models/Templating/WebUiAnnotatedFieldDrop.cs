using System.Collections.Generic;
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
    public class WebUiAnnotatedFieldDrop : AnnotatedFieldDrop
    {
        public string DisplayName { get; set; }

        public bool ShowDropdown { get; set; }
        public bool IsDisplayField { get; set; }

        public bool DisplayInList { get; set; }

        public WebUiAnnotatedFieldDrop(Field field) : base(field)
        {
            // assign default values to helper properties in case they are not filled later
            DisplayName = Name;

            this.ParseFieldAnnotations(field.Annotations);
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Considers two fields different if their types' EntityNames differ.
    /// </summary>
    public class WebUiAnnotatedFieldDropNameComparer : IEqualityComparer<WebUiAnnotatedFieldDrop>
    {
        public bool Equals(WebUiAnnotatedFieldDrop x, WebUiAnnotatedFieldDrop y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Type.EntityName == y.Type.EntityName;
        }

        public int GetHashCode(WebUiAnnotatedFieldDrop obj)
        {
            return ReferenceEquals(obj, null) ? 0 : obj.GetHashCode();
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Considers two fields equal if their types' EntityNames and IsArray fields are the same.
    /// </summary>
    public class WebUiAnnotatedFieldDropFullTypeComparer : IEqualityComparer<WebUiAnnotatedFieldDrop>
    {
        public bool Equals(WebUiAnnotatedFieldDrop x, WebUiAnnotatedFieldDrop y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            if (x.Type.EntityName != y.Type.EntityName)
                return false;

            return x.Type.IsArray == y.Type.IsArray;
        }

        public int GetHashCode(WebUiAnnotatedFieldDrop obj)
        {
            if (ReferenceEquals(obj, null)) return 0;

            var hashName = obj.Type.EntityName.GetHashCode();

            var hashIsArray = obj.Type.IsArray.GetHashCode();

            return hashName ^ hashIsArray;
        }
    }
}
