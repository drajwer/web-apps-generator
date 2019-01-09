using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.WebUi.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Extensions
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
                    case "DisplayName":
                        annotatedEntityDrop.DisplayName = (string)annotation.Params.First(p => p.Name == "Name").Value;
                        break;
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
                    case "DisplayName":
                        annotatedFieldDrop.DisplayName = (string)annotation.Params.First(p => p.Name == "Name").Value;
                        break;
                }
            }
        }
    }
}
