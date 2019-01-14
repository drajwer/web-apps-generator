using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services.Validators;
using Type = WebAppsGenerator.Core.Models.Type;

namespace WebAppsGenerator.Tests.Validation.AnnotationValidatorTests
{
    [TestClass]
    public class PropAnnotationValidatorTests
    {
        // tests causing exceptions
        [TestMethod]
        public void UnknownAnnotation_ThrowsUnknownPropAnnotationException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann2", new (string name, TypeKind type)[0]);
            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann1", true,
                    null, null)
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<UnknownPropAnnotationException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void UnknownAnnotationForFieldType_ThrowsInvalidAnnotationForPropTypeException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann", new (string name, TypeKind type)[0]);
            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    new [] {TypeKind.Int},
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationForPropTypeException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void RequiredParamIsOmitted_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann", new (string name, TypeKind type)[0]);
            var definitions = new List<AnnotationDefinition>
                {
                    PrepareAnnotationDefinition("Ann", true,
                        null,
                        new (string name, TypeKind type, bool isRequired)[] { ("param", TypeKind.Bool, true) })
                };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void UnknownParamIsUsed_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Bool) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null,
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void InvalidRequiredParamType_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Bool) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    null,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, true)})
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void InvalidUnrequiredParamType_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Bool) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, false)})
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void UsesNoParamsWhenForbidden_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    null,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, true)})
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void OneCommonParamCausesAmbiguity_ThrowsAmbigiousAnnotationException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Int) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    null,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param2", TypeKind.Int, false),
                    }),
                PrepareAnnotationDefinition("Ann", false,
                    null,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param3", TypeKind.Int, false),
                    }),
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<AmbigiousAnnotationException>(() => validator.ValidatePropAnnotations(entity));
        }

        [TestMethod]
        public void NoParamsCausesAmbiguity_ThrowsAmbigiousAnnotationException()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param2", TypeKind.Int, false),
                    }),
                PrepareAnnotationDefinition("Ann", true,
                    null,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param3", TypeKind.Int, false),
                    }),
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<AmbigiousAnnotationException>(() => validator.ValidatePropAnnotations(entity));
        }

        // tests checking valid input

        [TestMethod]
        public void UsesNoParamsWhenAllowed()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, false)})
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidatePropAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        [TestMethod]
        public void UsesAllRequiredParams()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Int) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    null,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, true),
                        ("param2", TypeKind.Int, false),
                    })
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidatePropAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        [TestMethod]
        public void HasNotAllowedPropTypeKindsDefined()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Int, "Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null,
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidatePropAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        [TestMethod]
        public void HasAllowedPropTypeKindsDefined()
        {
            // Arrange
            var entity = PrepareEntity(TypeKind.Bool, "Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    new [] { TypeKind.Bool }, 
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidatePropAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        private Entity PrepareEntity(TypeKind fieldType, string annName, (string name, TypeKind type)[] paramsArray)
        {
            var entity = new Entity(-1, -1)
            {
                Fields = new List<Field>
                {
                    new Field(-1, -1)
                    {
                        Type = new Type(-1, -1)
                        {
                            BaseTypeKind = fieldType,
                        },
                        Annotations = new List<Annotation>
                        {
                            new Annotation(-1, -1)
                            {
                                Name = annName,
                                Params = paramsArray
                                    .Select(p => new AnnotationParam(-1, -1) { Name = p.name, Type = p.type }).ToList(),
                            },
                        }
                    }
                }
            };
            return entity;
        }

        private AnnotationDefinition PrepareAnnotationDefinition(string annName, bool allowNoParams,
            TypeKind[] allowedPropTypeKinds, (string name, TypeKind type, bool isRequired)[] paramsArray)
        {
            var annotationDefinition = new AnnotationDefinition()
            {
                Name = annName,
                IsClassAnnotation = false,
                AllowNoParams = allowNoParams,
                Params = paramsArray?
                    .Select(p =>
                        new AnnotationParamDefinition { Name = p.name, Type = p.type, IsRequired = p.isRequired })
                    .ToList(),
                AllowedPropTypeKinds = allowedPropTypeKinds?.ToList(),

            };

            return annotationDefinition;
        }

        private AnnotationValidator PrepareValidator(IEnumerable<AnnotationDefinition> annotationDefinitions)
        {
            var annotationOptions = new AnnotationOptions()
            {
                Annotations = annotationDefinitions.ToList()
            };

            var options = Options.Create(annotationOptions);
            var exceptionHandler = new ImmediateExceptionHandler();
            var annotationValidator = new AnnotationValidator(options, exceptionHandler);

            return annotationValidator;
        }
    }
}
