using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Parsing.Annotations
{
    public class BasicAnnotationParamParser : IAnnotationParamParser
    {
        public AnnotationParam ParseAnnotationParam(string name, string valueString)
        {
            (TypeKind kind, object value) = ParseValueString(valueString);
            return new AnnotationParam()
            {
                Name = name,
                Value = value,
                Type = kind
            };
        }

        public (TypeKind, object) ParseValueString(string valueString)
        {
            if (valueString.StartsWith('"') && valueString.EndsWith('"'))
            {
                valueString = valueString.Trim('"');
                if (DateTimeOffset.TryParse(valueString, out var dateResult))
                    return (TypeKind.Date, dateResult);

                return (TypeKind.String, valueString);
            }

            if (int.TryParse(valueString, out var intResult))
                return (TypeKind.Int, intResult);
            if (double.TryParse(valueString, out var doubleResult))
                return (TypeKind.Float, doubleResult);
            if (bool.TryParse(valueString, out var boolResult))
                return (TypeKind.Bool, boolResult);

            throw new ParsingException($"Invalid value: {valueString}");
        }
    }
}
