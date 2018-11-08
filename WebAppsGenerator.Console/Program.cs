using Antlr4.Runtime;
using WebAppsGenerator.Core.Files;
using WebAppsGenerator.Core.Files.Providers;
using WebAppsGenerator.Core.Grammar;
using WebAppsGenerator.Core.Parsing.Annotations;
using WebAppsGenerator.Core.Parsing.Types;
using WebAppsGenerator.Generator.Validation;

namespace WebAppsGenerator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcatFileService concatFileService = new ConcatFileService(new FlatDirectoryFilesProvider("./../../../TestDir", "txt"));
            AntlrInputStream inputStream = new AntlrInputStream(concatFileService.ConcatFile);
            var lexer = new SneakLexer(inputStream);
        
            var commonTokenStream = new CommonTokenStream(lexer);
            
            var parser = new SneakParser(commonTokenStream);
            SneakParser.FileContext fileContext = parser.file();

            var visitor = new SneakParserCustomVisitor(new BasicTypeParser(), new BasicAnnotationParamParser());

            visitor.Visit(fileContext);

            var validator = new Validator();
            validator.ValidateTypes(visitor.Entities.Values);

            foreach (var token in commonTokenStream.GetTokens())
                System.Console.WriteLine($"{token},");
            System.Console.WriteLine(concatFileService.ConcatFile);

            System.Console.ReadKey();
        }
    }
}
