﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Files.Services;
using WebAppsGenerator.Core.Grammar;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Parsing.Annotations;
using WebAppsGenerator.Core.Parsing.Types;
using WebAppsGenerator.Core.Services;

namespace WebAppsGenerator.Core.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntlrModelProvider(this IServiceCollection services)
        {
            services.AddScoped<IInputModelProvider, AntlrInputModelProvider>();

            services.AddScoped<SneakParserMappingVisitor>();
            services.AddScoped<ParserProvider>();
            services.AddScoped<LexerProvider>();
            services.AddScoped<ConcatFileService>();

            services.AddScoped<ITypeParser, BasicTypeParser>();
            services.AddScoped<IAnnotationParamParser, BasicAnnotationParamParser>();


            return services;
        }

    }
}
