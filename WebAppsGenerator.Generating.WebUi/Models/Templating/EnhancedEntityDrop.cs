using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    public class EnhancedEntityDrop : EntityDrop
    {
        public EnhancedEntityDrop(Entity entity) : base(entity)
        {
            CamelCasePluralName = char.ToLowerInvariant(PluralName[0]) + PluralName.Substring(1);
            CamelCaseName = char.ToLowerInvariant(Name[0]) + Name.Substring(1);
            SnakeUppercasePluralName = ToSnakeUppercase(PluralName);
            SnakeUppercaseName = ToSnakeUppercase(Name);
        }

        public string CamelCasePluralName { get; }
        public string CamelCaseName { get; }
        public string SnakeUppercasePluralName { get; }
        public string SnakeUppercaseName { get; }


        private static string ToSnakeUppercase(string input)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
        }
    }
}
