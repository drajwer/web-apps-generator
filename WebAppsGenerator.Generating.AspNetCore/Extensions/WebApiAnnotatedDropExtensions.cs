using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.AspNetCore.Const;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;

namespace WebAppsGenerator.Generating.AspNetCore.Extensions
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
        public static void ParseEntityAnnotations(this WebApiAnnotatedEntityDrop annotatedEntityDrop, IEnumerable<Annotation> annotations)
        {
        }

        /// <summary>
        /// Fills drop object's properties with data from field annotations
        /// </summary>
        /// <param name="annotatedFieldDrop">Object to be filled</param>
        /// <param name="annotations">Field's annotations</param>
        public static void ParseFieldAnnotations(this WebApiAnnotatedFieldDrop annotatedFieldDrop, IEnumerable<Annotation> annotations)
        {
            if (annotations == null) return;
            
            foreach (var annotation in annotations)
            {
                switch (annotation.Name)
                {
                    case AspNetCoreAnnotations.InverseProperty:
                        annotatedFieldDrop.InverseProperty = (string)annotation.Params.First(p => p.Name == "Name").Value;
                        break;
                    case AspNetCoreAnnotations.Index:
                        annotatedFieldDrop.Index = true;
                        break;
                }
            }
        }
    }
}
