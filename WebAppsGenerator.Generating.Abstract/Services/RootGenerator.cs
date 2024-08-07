﻿using System.Collections.Generic;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Root implementation of <see cref="IGenerator"/>. Runs all received generators after fixing entities.
    /// </summary>
    public class RootGenerator : IGenerator
    {
        private readonly IEnumerable<IGenerator> _generators;
        private readonly OverwriteFileGenerator _overwriteFileGenerator;
        private IEntitiesFixer _entitiesFixer;
        public RootGenerator(IEnumerable<IGenerator> generators, OverwriteFileGenerator overwriteFileGenerator, IEntitiesFixer entitiesFixer)
        {
            _generators = generators;
            _entitiesFixer = entitiesFixer;
            _overwriteFileGenerator = overwriteFileGenerator;
        }

        /// <summary>
        /// Runs all received generators after fixing entities.
        /// </summary>
        public void Generate(IEnumerable<Entity> entities)
        {
            _entitiesFixer.FixEntities(entities);

            foreach (var generator in _generators)
            {
                generator.Generate(entities);
            }
            _overwriteFileGenerator.Generate();
        }
    }
}
