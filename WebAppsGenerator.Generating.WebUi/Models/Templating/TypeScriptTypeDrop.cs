using System;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Drop exposing field's type for TypeScript templates
    /// </summary>
    public class TypeScriptTypeDrop : TypeDrop
    {
        public bool? IsFloat { get; set; }
        public TypeScriptTypeDrop(TypeDrop type) : base(type)
        {
            Name = GetFullTypeName(type);
            if (BaseTypeKind == TypeKind.Int || BaseTypeKind == TypeKind.Float)
                IsFloat = BaseTypeKind == TypeKind.Float;
        }

        private string GetFullTypeName(TypeDrop type)
        {
            return type.BaseTypeKind == TypeKind.Entity
                ? GetFullEntityTypeName(type)
                : GetSimpleTypeName(type);
        }

        private string GetSimpleTypeName(TypeDrop type)
        {
            switch (type.BaseTypeKind)
            {
                case TypeKind.Bool:
                    return "boolean";
                case TypeKind.Int:
                    return "number";
                case TypeKind.Float:
                    return "number";
                case TypeKind.String:
                    return "string";
                case TypeKind.DateTime:
                    return "Date";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetFullEntityTypeName(TypeDrop type)
        {
            return IsArray ? $"List<{type.EntityName}>" : type.EntityName;
        } 
    }
}

