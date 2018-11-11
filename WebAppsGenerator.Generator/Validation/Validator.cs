using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Exceptions;

namespace WebAppsGenerator.Generator.Validation
{
    public class Validator : IValidator
    {
        public void ValidateAnnotations(IEnumerable<Entity> entities, List<AnnotationDefinition> allowedAnnotations)
        {
            foreach (var entity in entities)
            {
                ValidateClassAnnotations(entity, allowedAnnotations);

                ValidatePropAnnotations(entity, allowedAnnotations);
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

        public void ValidateClassAnnotations(Entity entity, List<AnnotationDefinition> allowedAnnotations)
        {
            foreach (var entityAnnotation in entity.Annotations)
            {
                var annotation = allowedAnnotations.FirstOrDefault(ann =>
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

        public void ValidatePropAnnotations(Entity entity, List<AnnotationDefinition> allowedAnnotations)
        {
            foreach (var entityField in entity.Fields)
            {
                foreach (var entityFieldAnnotation in entityField.Annotations)
                {
                    var annotation = allowedAnnotations.FirstOrDefault(ann =>
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
