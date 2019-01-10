using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Models.Templating;
using static WebAppsGenerator.Generating.WebUi.Const.WebUiAnnotations;
namespace WebAppsGenerator.Generating.WebUi.Extensions
{
    /// <summary>
    /// Provides methods for passing annotation data into drop object's properties
    /// </summary>
    public static class WebUiAnnotatedDropExtensions
    {
        /// <summary>
        /// Fills drop object's properties with data from entity annotations
        /// </summary>
        /// <param name="annotatedEntityDrop">Object to be filled</param>
        /// <param name="annotations">Entity's annotations</param>
        public static void ParseEntityAnnotations(this WebUiAnnotatedEntityDrop annotatedEntityDrop, IEnumerable<Annotation> annotations)
        {
            if (annotations == null) return;

            foreach (var annotation in annotations)
            {
                switch (annotation.Name)
                {
                    case DisplayName:
                        annotatedEntityDrop.DisplayName = (string)annotation.Params.First(p => p.Name == "Name").Value;
                        annotatedEntityDrop.PluralDisplayName =
                            PluralizationHelper.Pluralize(annotatedEntityDrop.DisplayName);
                        break;
                    case HideInMenu:
                        annotatedEntityDrop.HideInMenu = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Fills drop object's properties with data from field annotations
        /// </summary>
        /// <param name="annotatedFieldDrop">Object to be filled</param>
        /// <param name="annotations">Field's annotations</param>
        public static void ParseFieldAnnotations(this WebUiAnnotatedFieldDrop annotatedFieldDrop, IEnumerable<Annotation> annotations)
        {
            if (annotations == null) return;

            foreach (var annotation in annotations)
            {
                switch (annotation.Name)
                {
                    case DisplayName:
                        annotatedFieldDrop.DisplayName = (string)annotation.Params.First(p => p.Name == "Name").Value;
                        break;
                }
            }
        }
    }
}
