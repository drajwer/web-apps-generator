using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class Annotation : BaseModel
    {
        public string Name { get; set; }
        public List<AnnotationParam> Params { get; set; }
        public bool IsClassAnnotation { get; set; }

        public Annotation(Annotation model) : base(model)
        {
        }

        public Annotation(int lineNumber, int charPositionInLine) : base(lineNumber, charPositionInLine)
        {
        }

        public static Annotation Create() => new Annotation(-1, -1);
    }
}
