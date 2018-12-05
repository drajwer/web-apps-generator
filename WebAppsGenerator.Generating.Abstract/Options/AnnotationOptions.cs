using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Provides configuration for annotations validator
    /// </summary>
    public class AnnotationOptions
    {
        public List<AnnotationDefinition> Annotations { get; set; }
    }
}
