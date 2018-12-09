using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class ModelService
    {
        public List<ModelDrop> CreateModelDrops(IEnumerable<Entity> entities)
        {
            var entityDrops = entities.Select(e => new ModelDrop(e)).ToList();

            ReplaceManyToManyWithJoins(entities, entityDrops);
            AddForeignKeys(entities, entityDrops);
            SetCSharpTypes(entities, entityDrops);

            return entityDrops;
        }

        private static void SetCSharpTypes(IEnumerable<Entity> entities, List<ModelDrop> entityDrops)
        {
            foreach (var entityDrop in entityDrops)
            {
                foreach (var fieldDrop in entityDrop.Fields)
                {
                    fieldDrop.Type = new CSharpTypeDrop(fieldDrop.Type);
                }
            }
        }

        private static void AddForeignKeys(IEnumerable<Entity> entities, List<ModelDrop> entityDrops)
        {
            foreach (var entityDrop in entityDrops)
            {
                var relationFields = entityDrop.Fields.Where(f => !f.Type.IsSimpleType && !f.Type.IsArray);
                List<FieldDrop> fieldsToAdd = new List<FieldDrop>();
                foreach (var relationField in relationFields)
                {
                    var referencedEntity = entities.First(e => e.Name == relationField.Type.EntityName);
                    var referencedIdField = referencedEntity.Fields.First(e => e.Name == EntitiesFixer.Id);
                    var idField = new Field()
                    {
                        Name = $"{relationField.Name}Id",
                        Type = new Core.Models.Type()
                        {
                            BaseTypeKind = referencedIdField.Type.BaseTypeKind
                        }
                    };
                    fieldsToAdd.Add(new FieldDrop(idField));
                }
                entityDrop.Fields.AddRange(fieldsToAdd);
            }
        }

        private static void ReplaceManyToManyWithJoins(IEnumerable<Entity> entities, List<ModelDrop> entityDrops)
        {
            foreach (var entity in entities)
            {
                var entityDrop = entityDrops.First(d => d.Name == entity.Name);
                var manyToManyFields = entityDrop.Fields.Where(f => f.Relation?.HasManyWithMany ?? false).ToList();
                foreach (var manyToManyField in manyToManyFields)
                {
                    entityDrop.Fields.Remove(manyToManyField);
                    var isCurrentPrimary = manyToManyField.Relation.Primary;
                    var secondEntityName = manyToManyField.Type.EntityName;
                    var joinTypeName = isCurrentPrimary
                        ? entity.Name + secondEntityName
                        : secondEntityName + entity.Name;
                    joinTypeName = PluralizationHelper.Pluralize(joinTypeName);
                    var joinField = new Field()
                    {
                        Name = manyToManyField.Name,
                        Relation = new Relation() { Primary = true, HasOne = false, WithOne = true },
                        Type = new Core.Models.Type()
                        { BaseTypeKind = TypeKind.Entity, EntityName = joinTypeName, IsArray = true }
                    };
                    entityDrop.Fields.Add(new FieldDrop(joinField));

                    var currentEntityRefField = new Field()
                    {
                        Name = entity.Name,
                        Relation = new Relation() { Primary = false, HasOne = true, WithOne = false },
                        Type = new Core.Models.Type() { BaseTypeKind = TypeKind.Entity, EntityName = entity.Name }
                    };

                    var secondEntityRefField = new Field()
                    {
                        Name = secondEntityName,
                        Relation = new Relation() { Primary = false, HasOne = true, WithOne = false },
                        Type = new Core.Models.Type() { BaseTypeKind = TypeKind.Entity, EntityName = secondEntityName }
                    };

                    var joinTableFields = isCurrentPrimary
                        ? new List<Field>() { currentEntityRefField, secondEntityRefField }
                        : new List<Field>() { secondEntityRefField, currentEntityRefField };


                    if (entityDrops.Any(e => e.Name == joinTypeName))
                        continue;

                    var joinEntity = new Entity()
                    {
                        Name = joinTypeName,
                        Fields = joinTableFields
                    };

                    entityDrops.Add(new ModelDrop(joinEntity, true));
                }
            }
        }
    }
}
