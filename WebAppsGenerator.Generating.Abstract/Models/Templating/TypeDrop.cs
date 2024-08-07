﻿using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Templating class to expose one field's type
    /// </summary>
    public class TypeDrop : Drop
    {
        public TypeDrop(Type type, bool nullableForEntities = true)
        {
            Name = type.FullTypeName;
            IsNullable = type.IsNullable;
            IsArray = type.IsArray;
            IsSimpleType = type.BaseTypeKind != TypeKind.Entity;
            EntityName = type.EntityName;
            PluralEntityName = PluralizationHelper.Pluralize(EntityName);
            BaseTypeKind = type.BaseTypeKind;

            if (!IsSimpleType)
            {
                IsNullable = nullableForEntities;
            }
        }

        public TypeDrop(TypeDrop typeDrop)
        {
            Name = typeDrop.Name;
            IsNullable = typeDrop.IsNullable;
            IsArray = typeDrop.IsArray;
            IsSimpleType = typeDrop.IsSimpleType;
            EntityName = typeDrop.EntityName;
            PluralEntityName = typeDrop.PluralEntityName;
            BaseTypeKind = typeDrop.BaseTypeKind;
        }

        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public bool IsArray { get; set; }
        public bool IsSimpleType { get; set; }
        public string EntityName { get; set; }
        public string PluralEntityName { get; set; }
        public TypeKind BaseTypeKind { get; set; }

    }
}
