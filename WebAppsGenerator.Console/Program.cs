using Antlr4.Runtime;
using System;
using WebAppsGenerator.Core.Grammar;

namespace WebAppsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var lexer = new SneakLexer(CharStreams.fromstring("class Hello"));
            var commonTokenStream = new CommonTokenStream(lexer);
            foreach (var token in lexer.GetAllTokens())
                Console.Write($"{token},");
            Console.WriteLine();
            var parser = new SneakParser(commonTokenStream);


            Console.WriteLine("Hello World!");
        }
    }
}
