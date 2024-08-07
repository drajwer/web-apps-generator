﻿using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using static WebAppsGenerator.Generating.Abstract.Const.AnnotationsConsts;
namespace WebAppsGenerator.Generating.Abstract.Extensions
{
    /// <summary>
    /// Provides methods for passing annotation data into drop object's properties
    /// </summary>
    public static class AnnotatedDropExtensions
    {
        /// <summary>
        /// Fills drop object's properties with data from entity annotations
        /// </summary>
        /// <param name="annotatedEntityDrop">Object to be filled</param>
        /// <param name="annotations">Entity's annotations</param>
        public static void ParseEntityAnnotations(this AnnotatedEntityDrop annotatedEntityDrop, IEnumerable<Annotation> annotations)
        {
            if (annotations == null) return;

            foreach (var annotation in annotations)
            {
                switch (annotation.Name)
                {
                }
            }
        }

        /// <summary>
        /// Fills drop object's properties with data from field annotations
        /// </summary>
        /// <param name="annotatedFieldDrop">Object to be filled</param>
        /// <param name="annotations">Field's annotations</param>
        public static void ParseFieldAnnotations(this AnnotatedFieldDrop annotatedFieldDrop, IEnumerable<Annotation> annotations)
        {
            if (annotations == null) return;

            foreach (var annotation in annotations)
            {
                switch (annotation.Name)
                {
                    case Length:
                        annotatedFieldDrop.AddLengthAnnotation(annotation);
                        break;
                    case Range:
                        annotatedFieldDrop.AddRangeAnnotation(annotation);
                        break;
                    case Required:
                        annotatedFieldDrop.Required = true;
                        break;

                }
            }
        }
    }
}
