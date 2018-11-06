using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WebAppsGenerator.Tests.Grammar
{
    [TestClass]
    public class GrammarTests
    {
        public SneakParser Setup([CallerMemberName] string filename = "")
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"WebAppsGenerator.Tests.Grammar.Files.{filename}.txt";

            // TODO: change reader to use embedded resources
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                var reader = new StreamReader(stream);
                AntlrInputStream inputStream = new AntlrInputStream(reader);
                var lexer = new SneakLexer(inputStream);

                var commonTokenStream = new CommonTokenStream(lexer);

                var parser = new SneakParser(commonTokenStream);
                SneakParser.FileContext fileContext = parser.file();
                return parser;
            }
        }

        [TestMethod]
        public void ComplexFileSuccess()
        {
            var parser = Setup();
            Assert.AreEqual(0, parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void TrailingSpaceBeforeClass()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void NoIndentBeforeProp()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void NoNewlineBetweeneProps()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void NoNewlineBetweenAnnotations()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void DifferentIndentLvlBetweenProps()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void PropHasNoType()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void PropHasLowerIndentLvlThanAnn()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void PropHasHigherIndentLvlThanAnn()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void TrailingSpaceBeforeSecondClass()
        {
            var parser = Setup();
            Assert.IsTrue(0 < parser.NumberOfSyntaxErrors);
        }

        [TestMethod]
        public void NoNewlineBeforeEof()
        {
            var parser = Setup();
            Assert.AreEqual(0, parser.NumberOfSyntaxErrors);
        }
    }
}
