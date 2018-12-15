using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using Type = WebAppsGenerator.Core.Models.Type;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class SecondEntityRelationDrop : RelationDrop
    {
        public TypeDrop SecondIdType { get; set; }
        public string SecondEntityName { get; set; }

        public SecondEntityRelationDrop(Relation relation, Type secondIdType, string secondEntityName) : base(relation)
        {
            SecondEntityName = secondEntityName;
            SecondIdType = new CSharpTypeDrop(new TypeDrop(secondIdType));
        }
        public SecondEntityRelationDrop(RelationDrop relation, TypeDrop secondIdType, string secondEntityName) : base(relation)
        {
            SecondEntityName = secondEntityName;
            SecondIdType = new CSharpTypeDrop(secondIdType);
        }
    }
}
