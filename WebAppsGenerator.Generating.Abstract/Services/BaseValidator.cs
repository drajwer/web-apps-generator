using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Default implementation of <see cref="T:WebAppsGenerator.Generating.Abstract.Interfaces.IValidator" />.
    /// </summary>
    public class BaseValidator : IValidator
    {
        private readonly AnnotationOptions _annotationOptions;

        public BaseValidator(IOptions<AnnotationOptions> annotationOptions)
        {
            _annotationOptions = annotationOptions.Value;
        }

        /// <summary>
        /// Validates types and annotations for given entities
        /// </summary>
        public void ValidateEntities(IEnumerable<Entity> entities)
        {
            ValidateIdProps(entities);
            ValidateTypes(entities);
            ValidateAnnotations(entities);
            ValidateProperties(entities);
        }

        /// <summary>
        /// Validates all annotations for given entities
        /// </summary>
        public void ValidateAnnotations(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                ValidateClassAnnotations(entity);
                ValidatePropAnnotations(entity);
            }
        }

        /// <summary>
        /// Validates all <see cref="TypeKind.Entity"/> types are pointing to existing entities
        /// </summary>
        public void ValidateTypes(IEnumerable<Entity> entities)
        {
            entities = entities.ToList();
            var declaredClasses = entities.Select(e => e.Name).ToList();

            foreach (var entity in entities)
            {
                foreach (var prop in entity.Fields)
                {
                    if (prop.Type.BaseTypeKind == TypeKind.Entity && !declaredClasses.Contains(prop.Type.EntityName))
                        throw new UnknownTypeException(prop.Type.EntityName);
                }
            }
        }

        /// <summary>
        /// Validates all entity's annotations have known names.
        /// Also validates that all params have known names and correct value.
        /// </summary>
        public void ValidateClassAnnotations(Entity entity)
        {
            foreach (var entityAnnotation in entity.Annotations)
            {
                var annotation = _annotationOptions.Annotations.FirstOrDefault(ann =>
                    ann.IsClassAnnotation && ann.Name == entityAnnotation.Name);

                if (annotation == null)
                    throw new UnknownClassAnnotationException(entityAnnotation.Name);

                foreach (var entityAnnotationParam in entityAnnotation.Params)
                {
                    var param = annotation.Params.FirstOrDefault(p => p.Name == entityAnnotationParam.Name);

                    if (param == null)
                        throw new UnknownAnnotationParameterException(entityAnnotationParam.Name,
                            entityAnnotation.Name);

                    if (param.Type != entityAnnotationParam.Type)
                        throw new InvalidAnnotationParameterValueTypeException(annotation.Name, param.Name,
                            entityAnnotationParam.Type, param.Type);
                }
            }
        }

        /// <summary>
        /// Validates all entity properties' annotations have known names.
        /// Also validates that all params have known names and correct value.
        /// </summary>
        public void ValidatePropAnnotations(Entity entity)
        {
            foreach (var entityField in entity.Fields)
            {
                foreach (var entityFieldAnnotation in entityField.Annotations)
                {
                    var annotation = _annotationOptions.Annotations.FirstOrDefault(ann =>
                        !ann.IsClassAnnotation && ann.Name == entityFieldAnnotation.Name);

                    if (annotation == null)
                        throw new UnknownPropAnnotationException(entityFieldAnnotation.Name);

                    if (!annotation.AllowedPropTypeKinds.Contains(entityField.Type.BaseTypeKind))
                        throw new InvalidAnnotationForPropTypeException(annotation.Name,
                            entityField.Type.BaseTypeKind);

                    foreach (var entityFieldAnnotationParam in entityFieldAnnotation.Params)
                    {
                        var param = annotation.Params.FirstOrDefault(p => p.Name == entityFieldAnnotationParam.Name);

                        if (param == null)
                            throw new UnknownAnnotationParameterException(entityFieldAnnotationParam.Name,
                                entityFieldAnnotation.Name);

                        if (param.Type != entityFieldAnnotationParam.Type)
                            throw new InvalidAnnotationParameterValueTypeException(annotation.Name, param.Name,
                                entityFieldAnnotationParam.Type, param.Type);
                    }
                }
            }
        }

        /// <summary>
        /// Validates if properties are unique for given entities
        /// </summary>
        private void ValidateProperties(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var dict = new HashSet<string>();
                foreach (var entityField in entity.Fields)
                {
                    if (dict.Contains(entityField.Name))
                        throw new MultiplePropException(entityField.Name, entity.Name);
                    dict.Add(entityField.Name);
                }
            }
        }

        /// <summary>
        /// Validates if no entity contains property Id or id
        /// </summary>
        /// <param name="entities"></param>
        private void ValidateIdProps(IEnumerable<Entity> entities)
        {
            if (entities.Any(entity => entity.Fields.Exists(f => f.Name.ToLowerInvariant() == "id")))
            {
                throw new IdPropExistsException();
            }
        }
    }
}
