using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Services
{
    public class EntitiesFixer : IEntitiesFixer
    {
        public const string Id = "Id";

        public void FixEntities(IEnumerable<Entity> entities)
        {
            FixIds(entities);
            FixRelations(entities);
        }

        private void FixIds(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Fields.All(f => f.Name != Id))
                {
                    entity.Fields.Add(new Field()
                    {
                        Name = Id,
                        Annotations = new List<Annotation>(),
                        Type = new Models.Type()
                        {
                            FullTypeName = "int",
                            BaseTypeKind = TypeKind.Int,
                        }
                    });
                }
            }
        }

        private void FixRelations(IEnumerable<Entity> entities)
        {
            var entityList = entities.ToList();
            foreach (var entity in entityList)
            {
                foreach (var entityField in entity.Fields)
                {
                    if(entityField.Type.BaseTypeKind != TypeKind.Entity)
                        continue;
                    var referencedEntity = entityList.First(e => e.Name == entityField.Type.EntityName);
                    var referencedFields = referencedEntity.Fields.Where(f => f.Type.EntityName == entity.Name).ToList();
                    var referencedField = referencedFields.FirstOrDefault();
                    if (referencedFields.Count() > 1)
                    {
                        referencedField = referencedFields.FirstOrDefault(f =>
                            f.Name.Contains(entityField.Name) || entityField.Name.Contains(f.Name));
                    }

                    entityField.Relation = new Relation()
                    {
                        HasOne = !entityField.Type.IsArray,
                        WithOne = !referencedField?.Type.IsArray ?? false,
                        Primary = referencedField == null || entity.Name.CompareTo(referencedEntity.Name) >= 0,
                        SecondFieldName = referencedField?.Name
                    };
                }
            }
        }
    }
}
