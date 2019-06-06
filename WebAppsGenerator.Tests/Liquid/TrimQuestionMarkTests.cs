using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Tests.Liquid
{
    [TestClass]
    public class TrimQuestionMarkTests
    {
        [TestMethod]
        public void NullText()
        {
            // Arrange
            string text = null;

            // Act
            var result = LiquidFilters.TrimQuestionMark(text);

            // Assert
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void EmptyText()
        {
            // Arrange
            const string text = "";

            // Act
            var result = LiquidFilters.TrimQuestionMark(text);

            // Assert
            Assert.IsTrue(result == "");
        }

        [TestMethod]
        public void NoQuestionMarkAtTheEndText()
        {
            // Arrange
            const string text = "double";

            // Act
            var result = LiquidFilters.TrimQuestionMark(text);

            // Assert
            Assert.IsTrue(result == text);
        }

        [TestMethod]
        public void QuestionMarkInTheMiddleText()
        {
            // Arrange
            const string text = "tex?t";

            // Act
            var result = LiquidFilters.TrimQuestionMark(text);

            // Assert
            Assert.IsTrue(result == text);
        }

        [TestMethod]
        public void QuestionMarkAtTheEndText()
        {
            // Arrange
            const string text = "double?";

            // Act
            var result = LiquidFilters.TrimQuestionMark(text);

            // Assert
            const string expected = "double";
            Assert.IsTrue(result == expected);
        }
    }
}
