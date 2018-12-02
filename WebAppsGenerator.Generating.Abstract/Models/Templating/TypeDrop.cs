using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class TypeDrop
    {
        public TypeDrop(Core.Models.Type type)
        {
            Name = GetFullTypeName(type);
            IsNullable = type.IsNullable;
            IsArray = type.IsArray;
        }

        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public bool IsArray { get; set; }

        private string GetFullTypeName(Core.Models.Type type) => type.BaseTypeKind == TypeKind.Entity
            ? type.EntityName
            : type.BaseTypeKind == TypeKind.DateTime
                ? type.BaseTypeKind.ToString("G")
                : type.BaseTypeKind.ToString("G").ToLower() + (IsNullable ? "?" : "") + (IsArray ? "[]" : "");
    }
}
