using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class Field : BaseModel
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public Relation Relation { get; set; }
        public List<Annotation> Annotations { get; set; }

        public Field(Field model) : base(model)
        {
        }

        public Field(int lineNumber, int charPositionInLine) : base(lineNumber, charPositionInLine)
        {
        }

        public static Field Create() => new Field(-1, -1);
    }
}
