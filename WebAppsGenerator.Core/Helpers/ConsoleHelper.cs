using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        public static void WriteInfo(string info)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(info);
            Console.ResetColor();
        }
    }
}
