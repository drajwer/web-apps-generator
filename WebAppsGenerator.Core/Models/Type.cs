using System;

namespace WebAppsGenerator.Core.Models
{
    public class Type
    {
        public string EntityName { get; set; }
        public TypeKind BaseTypeKind { get; set; }
        public bool IsNullable { get; set; }
        public bool IsArray { get; set; }

        
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
