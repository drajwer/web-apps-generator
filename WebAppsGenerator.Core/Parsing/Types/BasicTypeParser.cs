using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using Type = WebAppsGenerator.Core.Models.Type;

namespace WebAppsGenerator.Core.Parsing.Types
{
    public class BasicTypeParser : ITypeParser
    {
        private const string IntType = "int";
        private const string FloatType = "float";
        private const string DateType = "date";
        private const string StringType = "string";
        private const string BoolType = "bool";

        public bool IsSimpleType(string type) => ParseTypeKind(type) != TypeKind.Entity;

        public Type ParseTypeName(string typeName)
        {
            var kind = ParseTypeKind(typeName);
            var type = new Type() {BaseTypeKind = kind};
            EnsureArrayAndNullable(typeName, kind);

            if (kind == TypeKind.Entity)
            {
                type.IsArray = IsArrayType(typeName);
                type.EntityName = GetBaseType(typeName);
            }
            else
            {
                type.IsNullable = IsNullableType(typeName);
            }

            return type;
        }

        private TypeKind ParseTypeKind(string type)
        {
            string baseType = GetBaseType(type).ToLower();
            switch (baseType)
            {
                case IntType:
                    return TypeKind.Int;
                case FloatType:
                    return TypeKind.Float;
                case DateType:
                    return TypeKind.Date;
                case StringType:
                    return TypeKind.String;
                case BoolType:
                    return TypeKind.Bool;
                default:
                    return TypeKind.Entity;
            }
        }

        private static string GetBaseType(string type)
        {
            if (type.EndsWith("[]"))
            {
                var baseType = type.TrimEnd(']').TrimEnd('[');
                EnsureBaseType(type, baseType);
                return baseType;
            }
            if (type.EndsWith('?'))
            {
                var baseType = type.TrimEnd('?');
                EnsureBaseType(type, baseType);
                return baseType;
            }

            EnsureBaseType(type, type);
            return type;
        }

        private void EnsureArrayAndNullable(string type, TypeKind kind)
        {
            if (kind == TypeKind.Entity && type.EndsWith("?"))
                throw new InvalidTypeParsingException(type);
            if (type.EndsWith("[]"))
                throw new InvalidTypeParsingException(type);
        }
        private static void EnsureBaseType(string type, string baseType)
        {
            if (baseType.EndsWith('?') || baseType.EndsWith(']') || baseType.EndsWith('['))
                throw new InvalidTypeParsingException(type);
        }

        private static bool IsArrayType(string typeName)
        {
            return typeName.EndsWith("[]");
        }

        private static bool IsNullableType(string typeName)
        {
            return typeName.EndsWith('?');
        }
    }
}
