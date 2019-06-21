using System.Globalization;
using System.Text.RegularExpressions;

namespace WebAppsGenerator.Generating.WebUi.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts string from pascal format to kebab case 
        /// </summary>
        /// <param name="value">string in pascal case</param>
        /// <returns>The same string in kebab case</returns>
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
