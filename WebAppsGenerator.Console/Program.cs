using System;
using System.IO;
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
using WebAppsGenerator.Generating.Abstract.IoC;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.Abstract.Services.Validators;
using WebAppsGenerator.Generating.AspNetCore.IoC;
using WebAppsGenerator.Generating.WebUi.IoC;

namespace WebAppsGenerator.Console
{
    internal class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        private static void Main(string[] args)
        {
            AddConfiguration();
            ConfigureServices();

            var concatFileService = new ConcatFileService(new FlatDirectoryFilesProvider("./../../../TestDir", "sn"));
            var inputStream = new AntlrInputStream(concatFileService.ConcatFile);
            var lexer = new SneakLexer(inputStream);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new SneakLexerErrorListener(concatFileService));

            var commonTokenStream = new CommonTokenStream(lexer);

            var parser = new SneakParser(commonTokenStream);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new SneakParserErrorListener(concatFileService));

            var fileContext = parser.file();

            var visitor = (SneakParserMappingVisitor)ServiceProvider.GetService<ISneakParserVisitor<object>>();

            visitor.Visit(fileContext);
            var validator = ServiceProvider.GetService<RootValidator>();
            validator.Validate(visitor.Entities.Values);

            var fixer = ServiceProvider.GetService<IEntitiesFixer>();
            fixer.FixEntities(visitor.Entities.Values);

            // pass visitor's results to generator
            var generator = ServiceProvider.GetService<RootGenerator>();
            generator.Generate(visitor.Entities.Values);
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddOptions();
            services.AddConfigurationOptions(Configuration);
            services.AddScoped<ISneakParserVisitor<object>, SneakParserMappingVisitor>();
            services.AddTransient<ITypeParser, BasicTypeParser>();
            services.AddTransient<IAnnotationParamParser, BasicAnnotationParamParser>();
            services.AddScoped<IGeneratorConfiguration, GeneratorConfiguration>();
            services.AddScoped<ICommandLineService, CommandLineService>();
            services.AddScoped<LiquidTemplateService>();
            services.AddScoped<Generating.Abstract.Interfaces.IFileService, FileService>();
            services.AddScoped<IExceptionHandler, ImmediateExceptionHandler>();
            services.AddValidators();
            services.AddRootGenerator();
            services.AddScoped<IEntitiesFixer, EntitiesFixer>();
            services.AddAspNetCoreGenerator();
            services.AddWebUiCoreGenerator();

            ServiceProvider = services.BuildServiceProvider();
        }

        private static void AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");
            Configuration = builder.Build();
        }
    }
}
