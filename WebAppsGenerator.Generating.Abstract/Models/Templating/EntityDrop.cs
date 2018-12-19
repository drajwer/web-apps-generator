using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Templating class to expose one entity only
    /// </summary>
    public class EntityDrop : Drop
    {
        public string Name { get; }
        public string PluralName { get; }

        public List<FieldDrop> Fields { get;}
        public FieldDrop IdField { get; }
        public EntityDrop(Entity entity)
        {
            Name = entity.Name;
            PluralName = PluralizationHelper.Pluralize(Name);
            Fields = entity.Fields.Select(f => new FieldDrop(f)).ToList();
            IdField = Fields.FirstOrDefault(f => f.Name == "Id");

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
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString().ToUpper()));
        }
    }
}
