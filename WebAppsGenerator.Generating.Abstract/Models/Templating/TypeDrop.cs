using DotLiquid;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Templating class to expose one field's type
    /// </summary>
    public class TypeDrop : Drop
    {
        public TypeDrop(Type type)
        {
            Name = type.FullTypeName;
            IsNullable = type.IsNullable;
            IsArray = type.IsArray;
        }

        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public bool IsArray { get; set; }
        public string FullTypeName { get; set; }
    }
}
