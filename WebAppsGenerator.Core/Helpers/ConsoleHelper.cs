using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WebAppsGenerator.Core.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteError(string error)
        {
#if DEBUG
            Debug.WriteLine(error);
#endif
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        public static void WriteInfo(string info)
        {
#if DEBUG
            Debug.WriteLine(info);
#endif
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(info);
            Console.ResetColor();
        }
    }
}
