
namespace WebAppsGenerator.Core.Models
{
    public class Type : BaseModel
    {
        public string EntityName { get; set; }
        public TypeKind BaseTypeKind { get; set; }
        public bool IsNullable { get; set; }
        public bool IsArray { get; set; }

        public string FullTypeName  { get; set; }

        public Type(BaseModel model) : base(model)
        {
        }

        public Type(int lineNumber, int charPositionInLine) : base(lineNumber, charPositionInLine)
        {
        }

        public static Type Create() => new Type(-1, -1);
    }

    public enum TypeKind
    {
        Bool,
        Int,
        Float,
        String,
        DateTime,
        Entity
    }   
}
