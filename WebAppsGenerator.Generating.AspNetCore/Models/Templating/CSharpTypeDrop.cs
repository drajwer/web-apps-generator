using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using Type = WebAppsGenerator.Core.Models.Type;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class CSharpTypeDrop : TypeDrop
    {
        public CSharpTypeDrop(TypeDrop type) : base(type)
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
                    typeName = "bool";
                    break;
                case TypeKind.Int:
                    typeName = "int";
                    break;
                case TypeKind.Float:
                    typeName = "double";
                    break;
                case TypeKind.DateTime:
                    typeName = "DateTimeOffset";
                    break;
                case TypeKind.String:
                    return "string";
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

