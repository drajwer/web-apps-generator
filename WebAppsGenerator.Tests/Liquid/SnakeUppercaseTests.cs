using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Tests.Liquid
{
    [TestClass]
    public class SnakeUppercaseTests
    {
        [TestMethod]
        public void NullText()
        {
            // Arrange
            string text = null;

            // Act
            var result = LiquidFilters.SnakeUppercase(text);

            // Assert
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void EmptyText()
        {
            // Arrange
            const string text = "";

            // Act
            var result = LiquidFilters.SnakeUppercase(text);

            // Assert
            Assert.IsTrue(result == "");
        }

        [TestMethod]
        public void DecapitalizedText()
        {
            // Arrange
            const string text = "someDecapitalizedText";

            // Act
            var result = LiquidFilters.SnakeUppercase(text);

            // Assert
            const string expected = "SOME_DECAPITALIZED_TEXT";
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void CapitalizedText()
        {
            // Arrange
            const string text = "SomeDecapitalizedText";

            // Act
            var result = LiquidFilters.SnakeUppercase(text);

            // Assert
            const string expected = "SOME_DECAPITALIZED_TEXT";
            Assert.IsTrue(result == expected);
        }
    }
}
