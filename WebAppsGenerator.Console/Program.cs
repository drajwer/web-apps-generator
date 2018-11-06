using Antlr4.Runtime;
using System;
using System.IO;
using System.Text;
using WebAppsGenerator.Core.Grammar;
using WebAppsGenerator.Core.Parsing.Annotations;
using WebAppsGenerator.Core.Parsing.Types;

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

            var visitor = new SneakParserCustomVisitor(new BasicTypeParser(), new BasicAnnotationParamParser());

            visitor.Visit(fileContext);

            foreach (var token in commonTokenStream.GetTokens())
                Console.WriteLine($"{token},");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
