namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Extension of <see cref="RelationDrop"/> with information about relation's second end
    /// </summary>
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
