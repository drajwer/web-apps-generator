using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Misc;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Grammar
{
    public class SneakParserCustomVisitor : SneakParserBaseVisitor<string>
    {
        public Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();
        public override string VisitClassDef([NotNull] SneakParser.ClassDefContext context)
        {
            var className = context.ID().ToString();
            var entity = new Entity()
            {
                Name = className,
            };

            if(!Entities.TryAdd(className, entity))
            {
                Console.WriteLine("Error");
            }
            return base.VisitClassDef(context);
        }
    }
}
