using System;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using static WebAppsGenerator.Generating.Abstract.Const.AnnotationsConsts;

namespace WebAppsGenerator.Generating.Abstract.Extensions
{
    /// <summary>
    /// Provides methods for passing length and range annotation data into drop object's properties
    /// </summary>
    public static class RangeAnnotationExtensions
    {
        public static void AddLengthAnnotation(this AnnotatedFieldDrop fieldDrop, Annotation annotation)
        {
            var min = GetParamValue<int>(annotation, Min);
            var max = GetParamValue<int>(annotation, Max);

            fieldDrop.LengthRange = new IntRangeDrop(min, max);
        }

        public static void AddRangeAnnotation(this AnnotatedFieldDrop fieldDrop, Annotation annotation)
        {
            switch (fieldDrop.Type.BaseTypeKind)
            {
                case TypeKind.Int:
                    AddIntRangeAnnotation(fieldDrop, annotation);
                    break;
                case TypeKind.Float:
                    AddDoubleRangeAnnotation(fieldDrop, annotation);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldDrop.Type));
            }
        }

        private static void AddIntRangeAnnotation(AnnotatedFieldDrop fieldDrop, Annotation annotation)
        {
            var min = GetParamValue<int>(annotation, Min);
            var max = GetParamValue<int>(annotation, Max);

            fieldDrop.ValueRange = new IntRangeDrop(min, max);
        }
        private static void AddDoubleRangeAnnotation(AnnotatedFieldDrop fieldDrop, Annotation annotation)
        {
            var min = GetParamValue<double>(annotation, Min);
            var max = GetParamValue<double>(annotation, Max);

            fieldDrop.ValueRange = new DoubleRangeDrop(min, max);
        }

        private static T? GetParamValue<T>(Annotation annotation, string param) where T : struct
        {
            return (T?)annotation.Params.FirstOrDefault(p => p.Name == param)?.Value;
        }
    }
}
