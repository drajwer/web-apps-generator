using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Extensions;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <inheritdoc />
    /// <summary>
    /// Extends EntityDrop by adding properties that store information extracted from annotations
    /// </summary>
    public abstract class AnnotatedEntityDrop : EntityDrop
    {
        protected AnnotatedEntityDrop(Entity entity) : base(entity)
        {
            this.ParseEntityAnnotations(entity.Annotations);
        }
    }
}
