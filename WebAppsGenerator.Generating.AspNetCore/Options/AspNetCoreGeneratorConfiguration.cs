﻿using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.AspNetCore.Options
{
    public class AspNetCoreGeneratorConfiguration : IGeneratorConfiguration
    {
        private readonly IGeneratorConfiguration _baseConfig;
        public string ProjectName => _baseConfig.ProjectName;
        public string OutputPath => _baseConfig.OutputPath;
        public string MigrationName => _baseConfig.MigrationName;
        public bool AddMigration => _baseConfig.AddMigration;
        public bool RunAspNetCoreGen => _baseConfig.RunAspNetCoreGen;
        public bool RunWebUiGen => _baseConfig.RunWebUiGen;
        public bool RunReactAppCreation => _baseConfig.RunReactAppCreation;
        public List<NuGetPackageDetails> CoreProjectPackages { get; }

        public AspNetCoreGeneratorConfiguration(IGeneratorConfiguration baseConfig, IOptions<AspNetCoreGeneratorOptions> options)
        {
            _baseConfig = baseConfig;
            CoreProjectPackages = options.Value?.CoreProjectPackages;
        }
    }
}
