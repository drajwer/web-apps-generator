using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class LiquidFilters
    {
        public static string DeCapitalize(string text)
        {
            if (text?.Length == 0)
                return text;
            if (text.Length == 1)
                return char.ToLower(text[0]).ToString();

            return $"{char.ToLower(text[0])}{text.Substring(1, text.Length - 1)}";
        }
        public static string SplitCamelCase(string text)
        {
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c) && i > 0)
                    result += ' ';
                result += c;
            }

            return result;
        }

        public static string SnakeUppercase(string text)
        {
            return string.Concat(text.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString().ToUpper()));
        }

        public static string TrimQuestionMark(string text)
        {
            return text.Trim('?');
        }
    }
}
