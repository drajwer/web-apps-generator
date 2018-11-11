using Antlr4.Runtime;
using Microsoft.Extensions.Configuration;
using WebAppsGenerator.Core.Files;
using WebAppsGenerator.Core.Files.Providers;
using WebAppsGenerator.Core.Grammar;
using WebAppsGenerator.Core.Parsing.Annotations;
using WebAppsGenerator.Core.Parsing.Types;

namespace WebAppsGenerator.Console
{
    internal class Program
    {
        public static IConfiguration Configuration { get; set; }

        private static void Main(string[] args)
        {
            var concatFileService = new ConcatFileService(new FlatDirectoryFilesProvider("./../../../TestDir", "txt"));
            var inputStream = new AntlrInputStream(concatFileService.ConcatFile);
            var lexer = new SneakLexer(inputStream);
        
            var commonTokenStream = new CommonTokenStream(lexer);
            
            var parser = new SneakParser(commonTokenStream);
            var fileContext = parser.file();

            var visitor = new SneakParserCustomVisitor(new BasicTypeParser(), new BasicAnnotationParamParser());

            visitor.Visit(fileContext);
            
            // pass visitor's results to generator
            var generator = new Generator.Generator.Generator();
            generator.ProcessVisitorResults(visitor.Entities.Values);

            foreach (var token in commonTokenStream.GetTokens())
                System.Console.WriteLine($"{token},");
            System.Console.WriteLine(concatFileService.ConcatFile);

            System.Console.ReadKey();
        }
    }
}
