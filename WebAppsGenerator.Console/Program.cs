using System;
using Antlr4.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Files;
using WebAppsGenerator.Core.Files.Providers;
using WebAppsGenerator.Core.Grammar;
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
        
            var commonTokenStream = new CommonTokenStream(lexer);
            
            var parser = new SneakParser(commonTokenStream);
            var fileContext = parser.file();

            var visitor = (SneakParserCustomVisitor) ServiceProvider.GetService<ISneakParserVisitor<object>>();// new SneakParserCustomVisitor(new BasicTypeParser(), new BasicAnnotationParamParser());

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
            services.AddScoped<IGenerator, Generator.Generator.Generator>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
