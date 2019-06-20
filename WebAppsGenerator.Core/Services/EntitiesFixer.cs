using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Services
{
    public class EntitiesFixer : IEntitiesFixer
    {
        public const string Id = "Id";

        private readonly IExceptionHandler _exceptionHandler;

        public EntitiesFixer(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Prepares entities for further processing by Id property creation and defining relations between entities
        /// </summary>
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

                    bool primary;
                    Field referencedField;

                    if (referencedFields.Count == 1)
                    {
                        referencedField = referencedFields.First();
                        if (!entityField.Type.IsArray && referencedField.Type.IsArray)      // one to many
                            primary = false;
                        else if (entityField.Type.IsArray && !referencedField.Type.IsArray) // many to one
                            primary = true;
                        else                                                                // many to many
                            primary = (!referencedField.Relation?.Primary) ?? true;
                    }
                    else
                    {
                        var inversePropAnnotation =
                                entityField.Annotations.FirstOrDefault(ann => ann.Name == "InverseProperty");

                        if (inversePropAnnotation != null)  // existence of this annotation indicates that this prop is principial
                        {
                            var referencedFieldName =
                                inversePropAnnotation.Params.First(p => p.Name == "Name").Value as string;

                            referencedField = referencedFields.First(f => f.Name == referencedFieldName);
                            primary = true;
                        }
                        else
                        {
                            referencedField = referencedFields
                                .FirstOrDefault(f => f.Annotations.Exists(ann =>
                                    ann.Name == "InverseProperty" &&
                                    (string)ann.Params.First(p => p.Name == "Name").Value == entityField.Name));

                            primary = false;

                            if (referencedField == null)
                            {
                                primary = true;
                                // EF won't be able to create migration
                            }
                        }

                    }

                    entityField.Relation = new Relation
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
