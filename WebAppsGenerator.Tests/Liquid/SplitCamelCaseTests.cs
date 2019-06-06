using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Tests.Liquid
{
    [TestClass]
    public class SplitCamelCaseTests
    {
        [TestMethod]
        public void NullText()
        {
            // Arrange
            string text = null;

            // Act
            var result = LiquidFilters.SplitCamelCase(text);

            // Assert
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void EmptyText()
        {
            // Arrange
            const string text = "";

            // Act
            var result = LiquidFilters.SplitCamelCase(text);

            // Assert
            Assert.IsTrue(result == "");
        }

        [TestMethod]
        public void DecapitalizedText()
        {
            // Arrange
            const string text = "splitDecapitalizedText";

            // Act
            var result = LiquidFilters.SplitCamelCase(text);

            // Assert
            const string expected = "split Decapitalized Text";
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void CapitalizedText()
        {
            // Arrange
            const string text = "SplitDecapitalizedText";

            // Act
            var result = LiquidFilters.SplitCamelCase(text);

            // Assert
            const string expected = "Split Decapitalized Text";
            Assert.IsTrue(result == expected);
        }
    }
}
