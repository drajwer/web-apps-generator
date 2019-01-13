namespace WebAppsGenerator.Core.Models
{
    public class AnnotationParam : BaseModel
    {
        public string Name { get; set; }
        public TypeKind Type { get; set; }
        public object Value { get; set; }

        public AnnotationParam(AnnotationParam model) : base(model)
        {
        }

        public AnnotationParam(int lineNumber, int charPositionInLine) : base(lineNumber, charPositionInLine)
        {
        }

        public static AnnotationParam Create() => new AnnotationParam(-1, -1);
    }
}
