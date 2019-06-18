using System.Linq;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Provides static methods to use in DotLiquid templates as filters
    /// </summary>
    public class LiquidFilters
    {
        public static string DeCapitalize(string text)
        {
            if (text == null)
                return null;

            if (text?.Length == 0)
                return text;

            if (text.Length == 1)
                return char.ToLower(text[0]).ToString();

            return $"{char.ToLower(text[0])}{text.Substring(1, text.Length - 1)}";
        }
        public static string SplitCamelCase(string text)
        {
            if (text == null)
                return null;

            var result = "";
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (char.IsUpper(c) && i > 0)
                    result += ' ';
                result += c;
            }

            return result;
        }

        public static string SnakeUppercase(string text)
        {
            if (text == null)
                return null;

            return string.Concat(text.Select((x, i) => i > 0 && char.IsUpper(x) ? ("_" + x.ToString()) : (x.ToString().ToUpper())));
        }

        public static string TrimQuestionMark(string text)
        {
            return text?.Trim('?');
        }
    }
}
