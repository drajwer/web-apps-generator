using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class LiquidFilters
    {
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
    }
}
