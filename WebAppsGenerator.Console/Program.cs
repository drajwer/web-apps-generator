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
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.IoC;
using WebAppsGenerator.Generating.AspNetCore.Services;

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

            var visitor = (SneakParserMappingVisitor) ServiceProvider.GetService<ISneakParserVisitor<object>>();

            visitor.Visit(fileContext);
            
            // pass visitor's results to generator
            var generator = ServiceProvider.GetService<IGenerator>();
            generator.Generate(visitor.Entities.Values);

            foreach (var token in commonTokenStream.GetTokens())
                System.Console.WriteLine($"{token},");
            System.Console.WriteLine(concatFileService.ConcatFile);

            System.Console.ReadKey();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddOptions();
            services.AddScoped<ISneakParserVisitor<object>, SneakParserMappingVisitor>();
            services.AddTransient<ITypeParser, BasicTypeParser>();
            services.AddTransient<IAnnotationParamParser, BasicAnnotationParamParser>();
            services.AddSingleton<IGeneratorConfiguration>(new GeneratorConfiguration()
                {OutputPath = "Output", ProjectName = "Bookstore"});
            services.AddScoped<ICommandLineService, CommandLineService>();
            services.AddScoped<LiquidTemplateService>();
            services.AddAspNetCoreGenerator();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
