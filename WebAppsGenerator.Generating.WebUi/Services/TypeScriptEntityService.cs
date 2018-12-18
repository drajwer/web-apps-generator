using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.WebUi.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Services
{
    public class TypeScriptEntityService
    {
        public List<EntityDrop> GetDrops(IEnumerable<Entity> entities)
        {
            var drops = entities.Select(e => new EntityDrop(e)).ToList();
            SetReferencedIdTypes(entities, drops);
            SetTypescriptTypes(drops);

            return drops;
        }

        private static void SetTypescriptTypes(List<EntityDrop> drops)
        {
            foreach (var entity in drops)
            {
                foreach (var entityField in entity.Fields)
                {
                    entityField.Name = entityField.Name.Substring(0, 1).ToLower() + entityField.Name.Substring(1, entityField.Name.Length - 1);
                    entityField.Type = new TypeScriptTypeDrop(entityField.Type);
                }
            }
        }

        private static void SetReferencedIdTypes(IEnumerable<Entity> entities, List<EntityDrop> entityDrops)
        {
            foreach (var entity in entities)
            {
                foreach (var entityField in entity.Fields)
                {
                    if (entityField.Type.BaseTypeKind == TypeKind.Entity && entityField.Relation != null)
                    {
                        var referencedEntity = entities.First(e => e.Name == entityField.Type.EntityName);
                        var referencedIdField = referencedEntity.Fields.First(f => f.Name == "Id");
                        var drop = entityDrops.First(e => e.Name == entity.Name).Fields.First(f => f.Name == entityField.Name);
                        drop.Relation = new SecondEntityRelationDrop(drop.Relation, new TypeScriptTypeDrop(new TypeDrop(referencedIdField.Type)), referencedEntity.Name);
                    }
                }
            }
        }
    }
}
