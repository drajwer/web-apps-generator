using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Generating.Abstract.Services.Validators
{
    /// <inheritdoc />
    /// <summary>
    /// Validates all entity's and properties' annotations have known names.
    /// Also validates that all params have known names and correct value.
    /// </summary>
    public class AnnotationValidator : IValidator
    {
        private readonly AnnotationOptions _annotationOptions;
        private readonly IExceptionHandler _exceptionHandler;

        public AnnotationValidator(IOptions<AnnotationOptions> annotationOptions, IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            _annotationOptions = annotationOptions.Value;
        }

        public void Validate(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                ValidateClassAnnotations(entity);
                ValidatePropAnnotations(entity);
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
                var annotationDefinitions = _annotationOptions.Annotations.Where(ann =>
                    ann.IsClassAnnotation && ann.Name == entityAnnotation.Name).ToList();

                if (annotationDefinitions.Count == 0)
                    _exceptionHandler.ThrowException(new UnknownClassAnnotationException(entityAnnotation.Name, entityAnnotation));

                annotationDefinitions = annotationDefinitions.Where(annDef =>
                {
                    var annDefParamsCount = annDef.Params?.Count ?? 0;
                    var hasNoLessPamsThanEntityUses = annDefParamsCount >= entityAnnotation.Params.Count;

                    if (!hasNoLessPamsThanEntityUses)
                        return false;

                    if (entityAnnotation.Params.Count == 0 && !annDef.AllowNoParams)
                        return false;

                    var allRequiredParamsAreUsedInEntity =
                        annDef.Params?.Where(param => param.IsRequired).All(requiredParam =>
                        {
                            return entityAnnotation.Params.Any(param =>
                                param.Name == requiredParam.Name && param.Type == requiredParam.Type);
                        }) ?? true;

                    if (!allRequiredParamsAreUsedInEntity)
                        return false;

                    var hasAllParamsUsedInEntity = entityAnnotation.Params.All(param =>
                    {
                        return annDef.Params?.Any(annDefParam =>
                                   param.Name == annDefParam.Name && param.Type == annDefParam.Type) ?? false;
                    });

                    return hasAllParamsUsedInEntity;
                }).ToList();

                if (annotationDefinitions.Count == 0)
                    _exceptionHandler.ThrowException(new InvalidAnnotationParametersException(entityAnnotation.Name, entityAnnotation));

                if (annotationDefinitions.Count > 1)
                    _exceptionHandler.ThrowException(new AmbigiousAnnotationException(entityAnnotation.Name, entityAnnotation));
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
                    var annotationDefinitions = _annotationOptions.Annotations.Where(ann =>
                        !ann.IsClassAnnotation && ann.Name == entityFieldAnnotation.Name).ToList();

                    if (annotationDefinitions.Count == 0)
                        _exceptionHandler.ThrowException(new UnknownPropAnnotationException(entityFieldAnnotation.Name, entityFieldAnnotation));

                    annotationDefinitions = annotationDefinitions
                        .Where(ann => ann.AllowedPropTypeKinds?.Contains(entityField.Type.BaseTypeKind) ?? true).ToList();

                    if (annotationDefinitions.Count == 0)
                        _exceptionHandler.ThrowException(new InvalidAnnotationForPropTypeException(entityFieldAnnotation.Name,
                            entityField.Type.BaseTypeKind, entityFieldAnnotation));

                    annotationDefinitions = annotationDefinitions.Where(annDef =>
                    {
                        var annDefParamsCount = annDef.Params?.Count ?? 0;
                        var hasNoLessPamsThanEntityUses = annDefParamsCount >= entityFieldAnnotation.Params.Count;

                        if (!hasNoLessPamsThanEntityUses)
                            return false;

                        if (entityFieldAnnotation.Params.Count == 0 && !annDef.AllowNoParams)
                            return false;

                        var allRequiredParamsAreUsedInEntity =
                            annDef.Params?.Where(param => param.IsRequired).All(requiredParam =>
                        {
                            return entityFieldAnnotation.Params.Any(param =>
                                param.Name == requiredParam.Name && param.Type == requiredParam.Type);
                        }) ?? true;

                        if (!allRequiredParamsAreUsedInEntity)
                            return false;

                        var hasAllParamsUsedInEntity = entityFieldAnnotation.Params.All(param =>
                        {
                            return annDef.Params?.Any(annDefParam =>
                                param.Name == annDefParam.Name && param.Type == annDefParam.Type) ?? false;
                        });

                        return hasAllParamsUsedInEntity;
                    }).ToList();

                    if (annotationDefinitions.Count == 0)
                        _exceptionHandler.ThrowException(new InvalidAnnotationParametersException(entityFieldAnnotation.Name, entityFieldAnnotation));

                    if (annotationDefinitions.Count > 1)
                        _exceptionHandler.ThrowException(new AmbigiousAnnotationException(entityFieldAnnotation.Name, entityFieldAnnotation));

                }
            }
        }
    }
}
