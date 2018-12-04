using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class BaseValidator : IValidator
    {
        private readonly AnnotationOptions _annotationOptions;

        public BaseValidator(IOptions<AnnotationOptions> annotationOptions)
        {
            _annotationOptions = annotationOptions.Value;
        }

        public void ValidateEntities(IEnumerable<Entity> entities)
        {
            ValidateTypes(entities);
            ValidateAnnotations(entities);
        }

        public void ValidateAnnotations(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                ValidateClassAnnotations(entity);
                ValidatePropAnnotations(entity);
            }
        }

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
    }
}
