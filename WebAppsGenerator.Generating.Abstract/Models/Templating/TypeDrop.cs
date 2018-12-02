using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class TypeDrop
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
