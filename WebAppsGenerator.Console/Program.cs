using System;
using Antlr4.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Files.FileSrevices;
using WebAppsGenerator.Core.Files.Providers;
using WebAppsGenerator.Core.Grammar;
using WebAppsGenerator.Core.Grammar.ErrorListeners;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Parsing.Annotations;
using WebAppsGenerator.Core.Parsing.Types;

namespace WebAppsGenerator.Console
{
    internal class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        private static void Main(string[] args)
        {
            ConfigureServices();

            var concatFileService = new ConcatFileService(new FlatDirectoryFilesProvider("./../../../TestDir", "txt"));
            var inputStream = new AntlrInputStream(concatFileService.ConcatFile);
            var lexer = new SneakLexer(inputStream);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new SneakLexerErrorListener(concatFileService));

            var commonTokenStream = new CommonTokenStream(lexer);
            
            var parser = new SneakParser(commonTokenStream);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new SneakParserErrorListener(concatFileService));

            var fileContext = parser.file();

            var visitor = (SneakParserCustomVisitor) ServiceProvider.GetService<ISneakParserVisitor<object>>();

            visitor.Visit(fileContext);
            
            // pass visitor's results to generator
            var generator = ServiceProvider.GetService<IGenerator>();
            generator.ProcessVisitorResults(visitor.Entities.Values);

            foreach (var token in commonTokenStream.GetTokens())
                System.Console.WriteLine($"{token},");
            System.Console.WriteLine(concatFileService.ConcatFile);

            System.Console.ReadKey();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddOptions();
            services.AddScoped<ISneakParserVisitor<object>, SneakParserCustomVisitor>();
            services.AddTransient<ITypeParser, BasicTypeParser>();
            services.AddTransient<IAnnotationParamParser, BasicAnnotationParamParser>();
            services.AddTransient<IGenerator, Generator.Generators.Generator>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
