using System;

namespace WebAppsGenerator.Core.Models
{
    public class Type
    {
        public string EntityName { get; set; }
        public TypeKind BaseTypeKind { get; set; }
        public bool IsNullable { get; set; }
        public bool IsArray { get; set; }

        public string FullTypeName  { get; set; }
        //=> BaseTypeKind == TypeKind.Entity
        //    ? EntityName
        //    : BaseTypeKind == TypeKind.DateTime
        //        ? BaseTypeKind.ToString("G")
        //        : BaseTypeKind.ToString("G").ToLower() + (IsNullable ? "?" : "") + (IsArray ? "[]" : "");
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
