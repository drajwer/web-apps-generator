using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Grammar
{
    public class SneakParserCustomVisitor : SneakParserBaseVisitor<object>
    {
        public Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();
        public override object VisitClassDef([NotNull] SneakParser.ClassDefContext context)
        {
            var className = context.ID().ToString();
            var fields = VisitBody(context.body());
            var entity = new Entity()
            {
                Name = className,
                Fields = (fields as Field[])?.ToList()
            };

            if(!Entities.TryAdd(className, entity))
            {
                Console.WriteLine("Error");
            }
            return entity;
        }
        public override object VisitBody([NotNull] SneakParser.BodyContext context)
        {
            if(context.properties() != null)
                return VisitProperties(context.properties());
            return new Field[0];
        }
        public override object VisitProperties([NotNull] SneakParser.PropertiesContext context)
        {
            var props = context.property().Select(ctx => VisitProperty(ctx)).OfType<Field>();

            return props.ToArray();
        }
        public override object VisitProperty([NotNull] SneakParser.PropertyContext context)
        {
            var property = new Field()
            {
                Name = context.ID().ToString(),
                Annotations = VisitAnnotations(context.annotations()) as List<Annotation>
            };

            return property;
        }

        public override object VisitAnnotations([NotNull] SneakParser.AnnotationsContext context)
        {
            return context.annotation().Select(ctx => VisitAnnotation(ctx)).OfType<Annotation>().ToList();
        }

        public override object VisitAnnotation([NotNull] SneakParser.AnnotationContext context)
        {
            return new Annotation()
            {
                Name = context.ID().ToString()
            };
        }
    }
}
