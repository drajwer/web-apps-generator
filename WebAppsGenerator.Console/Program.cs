using Antlr4.Runtime;
using System;
using System.IO;
using System.Text;

namespace WebAppsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader("./../../../../WebAppsGenerator.Core/Grammar/SimpleClass.txt");
            AntlrInputStream inputStream = new AntlrInputStream(reader);
            var lexer = new SneakLexer(inputStream);
        
            var commonTokenStream = new CommonTokenStream(lexer);
            
            var parser = new SneakParser(commonTokenStream);
            SneakParser.FileContext fileContext = parser.file();

            var visitor = new SneakParserBaseVisitor<string>();

            visitor.Visit(fileContext);

            foreach (var token in commonTokenStream.GetTokens())
                Console.WriteLine($"{token},");
            Console.WriteLine();

            Console.WriteLine("Hello World!");
        }
    }
}
