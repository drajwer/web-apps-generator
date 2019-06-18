using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    /// <summary>
    /// Extends <see cref="WebApiAnnotatedEntityDrop"/> with information about join classes created for many to many relations
    /// </summary>
    public class ModelDrop : WebApiAnnotatedEntityDrop
    {
        public bool IsJoinModel { get;  }
        public ModelDrop(Entity entity, bool isJoinModel = false) : base(entity)
        {
            IsJoinModel = isJoinModel;
        }
    }
}
