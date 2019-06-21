using System.Collections.Generic;
using WebAppsGenerator.Core.Grammar;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Services
{
    /// <summary>
    /// Provides business model from input files parsed using ANTLR.
    /// </summary>
    public class AntlrInputModelProvider : IInputModelProvider
    {
        private readonly ParserProvider _parserProvider;
        private readonly SneakParserMappingVisitor _visitor;

        public AntlrInputModelProvider(ParserProvider parserProvider, SneakParserMappingVisitor visitor)
        {
            _parserProvider = parserProvider;
            _visitor = visitor;
        }

        public IEnumerable<Entity> CreateModel()
        {
            var parser = _parserProvider.CreateParser();
            var fileContext = parser.file();
            _visitor.VisitFile(fileContext);

            return _visitor.Entities.Values;
        }
    }
}
