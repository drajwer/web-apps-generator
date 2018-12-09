using DotLiquid;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    public class RelationDrop : Drop
    {
        public bool HasOne { get; }
        public bool HasMany { get;  }
        public bool WithOne { get;  }
        public bool WithMany { get;  }
        public bool Primary { get; }

        public bool HasOneWithOne => HasOne && WithOne;
        public bool HasOneWithMany => HasOne && WithMany;
        public bool HasManyWithOne => HasMany && WithOne;
        public bool HasManyWithMany => HasMany && WithMany;

        public RelationDrop(Relation relation)
        {
            HasOne = relation.HasOne;
            HasMany = relation.HasMany;

            WithOne = relation.WithOne;
            WithMany = relation.WithMany;

            Primary = relation.Primary;
        }
    }
}
