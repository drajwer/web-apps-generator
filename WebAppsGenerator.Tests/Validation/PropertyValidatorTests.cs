using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Services.Validators;
using WebAppsGenerator.Tests.Mocks;

namespace WebAppsGenerator.Tests.Validation
{
    [TestClass]
    public class PropertyValidatorTests
    {
        private ExceptionHandlerMock _exceptionHandler;
        private PropertyValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _exceptionHandler = new ExceptionHandlerMock();
            _validator = new PropertyValidator(_exceptionHandler);
        }

        [TestMethod]
        public void NonUniquePropsInClass()
        {
            // Arrange
            var entities = CreateEntities(false);

            // Act
            _validator.Validate(entities);

            // Assert
            Assert.IsTrue(_exceptionHandler.IsExceptionThrown<MultiplePropException>());
        }

        [TestMethod]
        public void NonUniquePropsWithDifferentTypesInClass()
        {
            // Arrange
            var entities = CreateEntities(false);
            entities.First().Fields[1].Type.BaseTypeKind = TypeKind.String;

            // Act
            _validator.Validate(entities);

            // Assert
            Assert.IsTrue(_exceptionHandler.IsExceptionThrown<MultiplePropException>());
        }

        [TestMethod]
        public void NonUniqueAcrossClasses()
        {
            // Arrange
            var entities = CreateEntities(true);
            entities.AddRange(CreateEntities(true));

            // Act
            _validator.Validate(entities);

            // Assert
            Assert.IsFalse(_exceptionHandler.Exceptions.Any());
        }

        private List<Entity> CreateEntities(bool uniqueProps)
        {
            return new List<Entity>()
            {
                new Entity()
                {
                    Fields = new List<Field>()
                    {
                        new Field()
                        {
                            Name = "Test",
                            Type = new Type() {BaseTypeKind = TypeKind.Int}
                        },
                        new Field()
                        {
                            Name = uniqueProps ? "Other" : "Test",
                            Type = new Type() {BaseTypeKind = TypeKind.Int}
                        },
                    }
                }
            };
        }
    }
}
