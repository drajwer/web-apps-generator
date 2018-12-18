using System;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    public class TypeScriptTypeDrop : TypeDrop
    {
        public TypeScriptTypeDrop(TypeDrop type) : base(type)
        {
            Name = GetFullTypeName(type);
        }

        private string GetFullTypeName(TypeDrop type)
        {
            return type.BaseTypeKind == TypeKind.Entity
                ? GetFullEntityTypeName(type)
                : GetSimpleTypeName(type);
        }

        private string GetSimpleTypeName(TypeDrop type)
        {
            string typeName = "";
            switch (type.BaseTypeKind)
            {
                case TypeKind.Bool:
                    typeName = "boolean";
                    break;
                case TypeKind.Int:
                    typeName = "number";
                    break;
                case TypeKind.Float:
                    typeName = "number";
                    break;
                case TypeKind.String:
                    return "string";
                case TypeKind.DateTime:
                    return "Date";
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (IsNullable)
                typeName += "?";

            return typeName;
        }

        private string GetFullEntityTypeName(TypeDrop type)
        {
            return IsArray ? $"List<{type.EntityName}>" : type.EntityName;
        } 
    }
}

