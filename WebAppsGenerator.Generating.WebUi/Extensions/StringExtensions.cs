﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace WebAppsGenerator.Generating.WebUi.Extensions
{
    public static class StringExtensions
    {
        public static string PascalToKebabCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                    value,
                    "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                    "_$1",
                    RegexOptions.Compiled)
                .Trim()
                .ToUpper(CultureInfo.InvariantCulture);
        }
    }
}
