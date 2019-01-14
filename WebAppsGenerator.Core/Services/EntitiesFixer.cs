using System;
using System.Collections.Generic;
using System.Linq;
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
            SortFields(entities);
        }

        private void SortFields(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var idField = entity.Fields.First(f => f.Name == Id);
                entity.Fields.Remove(idField);
                entity.Fields.Insert(0, idField);
            }
        }

        private void FixIds(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Fields.All(f => f.Name != Id))
                {
                    entity.Fields.Add(new Field(-1, -1)
                    {
                        Name = Id,
                        Annotations = new List<Annotation>(),
                        Type = new Models.Type(-1, -1)
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
                    if (entityField.Type.BaseTypeKind != TypeKind.Entity)
                        continue;
                    var referencedEntity = entityList.First(e => e.Name == entityField.Type.EntityName);
                    var referencedFields = referencedEntity.Fields.Where(f => f.Type.EntityName == entity.Name).ToList();
                    var referencedField = referencedFields.FirstOrDefault();
                    if (referencedFields.Count > 1)
                    {
                        var inversePropAnnotation =
                            entityField.Annotations.FirstOrDefault(ann => ann.Name == "InverseProperty");
                        if (inversePropAnnotation != null)
                            referencedField =
                                referencedFields.First(f =>
                                    f.Name == (string) inversePropAnnotation.Params
                                        .First(p => p.Name == "Name").Value);
                        else
                            referencedField = null;
                    }

                    bool primary;

                    var inversePropInEntityField =
                        entityField.Annotations.FirstOrDefault(ann => ann.Name == "InverseProperty");

                    var inversePropInReferencedField =
                        referencedField?.Annotations.FirstOrDefault(ann => ann.Name == "InverseProperty");

                    if (inversePropInEntityField != null && inversePropInReferencedField != null)
                        primary = string.Compare(entity.Name, referencedEntity.Name, StringComparison.Ordinal) >= 0;

                    else if (inversePropInEntityField != null) // the class with InverseProperty annotation declared is principial
                        primary = true;
                    else if (inversePropInReferencedField != null)
                        primary = false;
                    else
                        primary = false;

                    entityField.Relation = new Relation()
                    {
                        HasOne = !entityField.Type.IsArray,
                        WithOne = !referencedField?.Type.IsArray ?? false,
                        Primary = primary,
                        SecondFieldName = referencedField?.Name
                    };
                }
            }
        }
    }
}
