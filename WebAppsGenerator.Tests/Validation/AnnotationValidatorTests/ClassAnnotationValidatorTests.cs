using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services.Validators;

namespace WebAppsGenerator.Tests.Validation.AnnotationValidatorTests
{
    [TestClass]
    public class ClassAnnotationValidatorTests
    {
        // tests causing exceptions
        [TestMethod]
        public void UnknownAnnotation_ThrowsUnknownPropAnnotationException()
        {
            // Arrange
            var entity = PrepareEntity("Ann2", new (string name, TypeKind type)[0]);
            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann1", true,
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<UnknownClassAnnotationException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void RequiredParamIsOmitted_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity("Ann", new (string name, TypeKind type)[0]);
            var definitions = new List<AnnotationDefinition>
                {
                    PrepareAnnotationDefinition("Ann", true,
                        new (string name, TypeKind type, bool isRequired)[] { ("param", TypeKind.Bool, true) })
                };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void UnknownParamIsUsed_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Bool) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void InvalidRequiredParamType_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Bool) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, true)})
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void InvalidUnrequiredParamType_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Bool) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, false)})
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void UsesNoParamsWhenForbidden_ThrowsInvalidAnnotationParametersException()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, true)})
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<InvalidAnnotationParametersException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void OneCommonParamCausesAmbiguity_ThrowsAmbigiousAnnotationException()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Int) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param2", TypeKind.Int, false),
                    }),
                PrepareAnnotationDefinition("Ann", false,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param3", TypeKind.Int, false),
                    }),
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<AmbigiousAnnotationException>(() => validator.ValidateClassAnnotations(entity));
        }

        [TestMethod]
        public void NoParamsCausesAmbiguity_ThrowsAmbigiousAnnotationException()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param2", TypeKind.Int, false),
                    }),
                PrepareAnnotationDefinition("Ann", true,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, false),
                        ("param3", TypeKind.Int, false),
                    }),
            };
            var validator = PrepareValidator(definitions);

            // Act & Assert
            Assert.ThrowsException<AmbigiousAnnotationException>(() => validator.ValidateClassAnnotations(entity));
        }

        // tests checking valid input

        [TestMethod]
        public void UsesNoParamsWhenAllowed()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    new (string name, TypeKind type, bool isRequired)[] {("param", TypeKind.Int, false)})
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidateClassAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        [TestMethod]
        public void UsesAllRequiredParams()
        {
            // Arrange
            var entity = PrepareEntity("Ann",
                new (string name, TypeKind type)[] { ("param", TypeKind.Int) });

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", false,
                    new (string name, TypeKind type, bool isRequired)[]
                    {
                        ("param", TypeKind.Int, true),
                        ("param2", TypeKind.Int, false),
                    })
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidateClassAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        [TestMethod]
        public void CorrectlyChoosesAnnotation()
        {
            // Arrange
            var entity = PrepareEntity("Ann", new (string name, TypeKind type)[0]);

            var definitions = new List<AnnotationDefinition>
            {
                PrepareAnnotationDefinition("Ann", true,
                    null),
                PrepareAnnotationDefinition("Ann2", true,
                    null)
            };
            var validator = PrepareValidator(definitions);

            // Act
            validator.ValidateClassAnnotations(entity);

            // Assert
            // if no exception was thrown, assertion passed
        }

        private Entity PrepareEntity(string annName, (string name, TypeKind type)[] paramsArray)
        {
            var entity = new Entity(-1, -1)
            {
                Annotations = new List<Annotation>
                {
                    new Annotation(-1, -1)
                     {
                         Name = annName,
                         Params = paramsArray
                             .Select(p => new AnnotationParam(-1, -1) { Name = p.name, Type = p.type }).ToList(),
                     },
                }
            };
            return entity;
        }

        private AnnotationDefinition PrepareAnnotationDefinition(string annName, bool allowNoParams,
            (string name, TypeKind type, bool isRequired)[] paramsArray)
        {
            var annotationDefinition = new AnnotationDefinition()
            {
                Name = annName,
                IsClassAnnotation = true,
                AllowNoParams = allowNoParams,
                Params = paramsArray?
                    .Select(p =>
                        new AnnotationParamDefinition { Name = p.name, Type = p.type, IsRequired = p.isRequired })
                    .ToList(),
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
