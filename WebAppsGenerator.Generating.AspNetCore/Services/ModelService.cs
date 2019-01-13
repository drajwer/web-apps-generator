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
        private const string Id = "Id";
        public List<ModelDrop> CreateModelDrops(IEnumerable<Entity> entities)
        {
            var entityDrops = entities.Select(e => new ModelDrop(e)).ToList();

            ReplaceManyToManyWithJoins(entities, entityDrops);
            AddForeignKeys(entities, entityDrops);
            SetReferencedIdTypes(entities, entityDrops);
            SetCSharpTypes(entities, entityDrops);

            return entityDrops;
        }

        private static void SetReferencedIdTypes(IEnumerable<Entity> entities, List<ModelDrop> entityDrops)
        {
            foreach (var entity in entities)
            {
                foreach (var entityField in entity.Fields)
                {
                    if (entityField.Type.BaseTypeKind == TypeKind.Entity && entityField.Relation != null)
                    {
                        var referencedEntity = entities.First(e => e.Name == entityField.Type.EntityName);
                        var referencedIdField = referencedEntity.Fields.First(f => f.Name == Id);
                        var drop = entityDrops.First(e => e.Name == entity.Name).Fields.First(f => f.Name == entityField.Name);
                        drop.Relation = new SecondEntityRelationDrop(drop.Relation, new CSharpTypeDrop(new TypeDrop(referencedIdField.Type)), referencedEntity.Name);
                    }
                }
            }
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

                    
                    var idField = new Field(-1, -1)
                    {
                        Name = $"{relationField.Name}Id",
                        Type = new Core.Models.Type(-1, -1)
                        {
                            BaseTypeKind = referencedIdField.Type.BaseTypeKind,
                            IsNullable = !entityDrop.IsJoinModel
                        },
                    };
                    var drop = new WebApiAnnotatedFieldDrop(idField) {Relation = relationField.Relation};
                    fieldsToAdd.Add(drop);
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
                    var joinField = new Field(-1, -1)
                    {
                        Name = manyToManyField.Name, 
                        Type = new Core.Models.Type(-1, -1)
                        { BaseTypeKind = TypeKind.Entity, EntityName = joinTypeName, IsArray = true }
                    };
                    var drop = new WebApiAnnotatedFieldDrop(joinField) {Relation = manyToManyField.Relation};
                    entityDrop.Fields.Add(drop);

                    var currentEntityRefField = new Field(-1, -1)
                    {
                        Name = entity.Name,
                        Relation = new Relation() { Primary = false, HasOne = true, WithOne = false },
                        Type = new Core.Models.Type(-1, -1) { BaseTypeKind = TypeKind.Entity, EntityName = entity.Name }
                    };

                    var secondEntityRefField = new Field(-1, -1)
                    {
                        Name = secondEntityName,
                        Relation = new Relation() { Primary = false, HasOne = true, WithOne = false },
                        Type = new Core.Models.Type(-1, -1) { BaseTypeKind = TypeKind.Entity, EntityName = secondEntityName }
                    };

                    var joinTableFields = isCurrentPrimary
                        ? new List<Field>() { currentEntityRefField, secondEntityRefField }
                        : new List<Field>() { secondEntityRefField, currentEntityRefField };


                    if (entityDrops.Any(e => e.Name == joinTypeName))
                        continue;

                    var joinEntity = new Entity(-1, -1)
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
