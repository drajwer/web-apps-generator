using System;

namespace WebAppsGenerator.Core.Helpers
{
    /// <summary>
    /// Helper class for displaying messages in console
    /// </summary>
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

        public static bool Prompt(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{message}(y/N): ");
            var answer = Console.ReadLine();
            Console.ResetColor();

            if (answer.ToLower() == "y")
                return true;
            return false;
        }
    }
}
