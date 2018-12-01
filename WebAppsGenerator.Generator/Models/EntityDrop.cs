using System.Collections.Generic;
using DotLiquid;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generator.Models
{
    public class EntityDrop: Drop
    {
        private readonly Entity _entity;

        public string Name => _entity.Name;
        public List<Field> Fields => _entity.Fields;
        public List<Annotation> Annotations => _entity.Annotations;

        public EntityDrop(Entity entity)
        {
            _entity = entity;
        }
    }
}