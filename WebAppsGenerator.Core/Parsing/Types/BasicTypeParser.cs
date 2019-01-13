using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;
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
        private readonly IExceptionHandler _exceptionHandler;

        public BasicTypeParser(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public Type ParseTypeName(string typeName, int lineNo, int charNo)
        {
            var type = new Type(lineNo, charNo) {  FullTypeName = typeName };
            var kind = ParseTypeKind(type);
            type.BaseTypeKind = kind;
            EnsureArrayAndNullable(type);

            if (kind == TypeKind.Entity)
            {
                type.IsArray = IsArrayType(typeName);
                type.EntityName = GetBaseType(type);
            }
            else
            {
                type.IsNullable = IsNullableType(typeName);
            }

            return type;
        }

        private TypeKind ParseTypeKind(Type type)
        {
            string baseType = GetBaseType(type).ToLower();
            switch (baseType)
            {
                case IntType:
                    return TypeKind.Int;
                case FloatType:
                    return TypeKind.Float;
                case DateType:
                    return TypeKind.DateTime;
                case StringType:
                    return TypeKind.String;
                case BoolType:
                    return TypeKind.Bool;
                default:
                    return TypeKind.Entity;
            }
        }

        private string GetBaseType(Type type)
        {
            var typeName = type.FullTypeName;
            if (typeName.EndsWith("[]"))
            {
                var baseType = typeName.TrimEnd(']').TrimEnd('[');
                EnsureBaseType(type, baseType);
                return baseType;
            }
            if (typeName.EndsWith('?'))
            {
                var baseType = typeName.TrimEnd('?');
                EnsureBaseType(type, baseType);
                return baseType;
            }

            EnsureBaseType(type, typeName);
            return typeName;
        }

        private void EnsureArrayAndNullable(Type type)
        {
            var kind = type.BaseTypeKind;
            var typeName = type.FullTypeName;

            if (kind == TypeKind.Entity && typeName.EndsWith("?"))
                _exceptionHandler.ThrowException(new InvalidTypeParsingException(type));
            if (kind != TypeKind.Entity && typeName.EndsWith("[]"))                   
                _exceptionHandler.ThrowException(new InvalidTypeParsingException(type));
        }
        private void EnsureBaseType(Type type, string baseType)
        {
            if (baseType.EndsWith('?') || baseType.EndsWith(']') || baseType.EndsWith('['))
                _exceptionHandler.ThrowException(new InvalidTypeParsingException(type));
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
