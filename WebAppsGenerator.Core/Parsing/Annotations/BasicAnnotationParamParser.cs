using System;
using System.Globalization;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Parsing.Annotations
{
    public class BasicAnnotationParamParser : IAnnotationParamParser
    {
        public AnnotationParam ParseAnnotationParam(string name, string valueString, int lineNo, int charNo)
        {
            (TypeKind kind, object value) = ParseValueString(valueString, lineNo, charNo);
            return new AnnotationParam(lineNo, charNo)
            {
                Name = name,
                Value = value,
                Type = kind
            };
        }

        private (TypeKind, object) ParseValueString(string valueString, int lineNo, int charNo)
        {
            if (valueString.StartsWith('"') && valueString.EndsWith('"'))
            {
                valueString = valueString.Trim('"');
                if (DateTimeOffset.TryParse(valueString, out var dateResult))
                    return (TypeKind.DateTime, dateResult);

                return (TypeKind.String, valueString);
            }

            if (int.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intResult))
                return (TypeKind.Int, intResult);
            if (double.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out var doubleResult))
                return (TypeKind.Float, doubleResult);
            if (bool.TryParse(valueString, out var boolResult))
                return (TypeKind.Bool, boolResult);

            throw new ParsingException($"Invalid value: {valueString}", lineNo, charNo);
        }
    }
}
