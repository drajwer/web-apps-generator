namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class SecondEntityRelationDrop : RelationDrop
    {
        public TypeDrop SecondIdType { get; set; }
        public string SecondEntityName { get; set; }

        public SecondEntityRelationDrop(RelationDrop relation, TypeDrop secondIdType, string secondEntityName) : base(relation)
        {
            SecondEntityName = secondEntityName;
            SecondIdType = secondIdType;
        }
    }
}
