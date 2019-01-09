using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Extensions;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends FieldDrop by adding properties that store information extracted from annotations
    /// </summary>
    public abstract class AnnotatedFieldDrop : FieldDrop
    {
        public IntRangeDrop LengthRange { get; set; }
        public IRangeDrop ValueRange { get; set; }
        public bool Required { get; set; }

        protected AnnotatedFieldDrop(Field field) : base(field)
        {
            this.ParseFieldAnnotations(field.Annotations);
        }
    }
}
