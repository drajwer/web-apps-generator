using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Tests.Liquid
{
    [TestClass]
    public class DecapitalizeTests
    {
        [TestMethod]
        public void NullText()
        {
            // Arrange
            string text = null;

            // Act
            var result = LiquidFilters.DeCapitalize(text);

            // Assert
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void EmptyText()
        {
            // Arrange
            const string text = "";

            // Act
            var result = LiquidFilters.DeCapitalize(text);

            // Assert
            Assert.IsTrue(result == "");
        }

        [TestMethod]
        public void CapitalLetter()
        {
            // Arrange
            const string text = "D";

            // Act
            var result = LiquidFilters.DeCapitalize(text);

            // Assert
            Assert.IsTrue(result == "d");
        }

        [TestMethod]
        public void LowercaseLetter()
        {
            // Arrange
            const string text = "r";

            // Act
            var result = LiquidFilters.DeCapitalize(text);

            // Assert
            Assert.IsTrue(result == "r");
        }

        [TestMethod]
        public void DecapitalizedText()
        {
            // Arrange
            const string text = "decapitalizedText";

            // Act
            var result = LiquidFilters.DeCapitalize(text);

            // Assert
            Assert.IsTrue(result == text);
        }

        [TestMethod]
        public void CapitalizedText()
        {
            // Arrange
            const string text = "CapitalizedText";

            // Act
            var result = LiquidFilters.DeCapitalize(text);

            // Assert
            const string expected = "capitalizedText";
            Assert.IsTrue(result == expected);
        }

    }
}
