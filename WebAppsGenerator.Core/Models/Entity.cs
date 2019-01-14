using System.Collections.Generic;

namespace WebAppsGenerator.Core.Models
{
    public class Entity : BaseModel
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
        public List<Annotation> Annotations { get; set; }

        public Entity(Entity model) : base(model)
        {
        }

        public Entity(int lineNumber, int charPositionInLine) : base(lineNumber, charPositionInLine)
        {
        }

        public static Entity Create() => new Entity(-1, -1); 
    }
}
