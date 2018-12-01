using System;
using System.Linq;

namespace WebAppsGenerator.Generator.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new [] {
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}