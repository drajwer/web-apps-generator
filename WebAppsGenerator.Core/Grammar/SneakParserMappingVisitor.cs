using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Parsing.Annotations;
using WebAppsGenerator.Core.Parsing.Types;
using Type = WebAppsGenerator.Core.Models.Type;

namespace WebAppsGenerator.Core.Grammar
{
    public class SneakParserMappingVisitor : SneakParserBaseVisitor<object>
    {
        private readonly ITypeParser _typeParser;
        private readonly IAnnotationParamParser _annotationParamParser;

        public Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();

        public SneakParserMappingVisitor(ITypeParser typeParser, IAnnotationParamParser annotationParamParser)
        {
            _typeParser = typeParser;
            _annotationParamParser = annotationParamParser;
        }

        public override object VisitClassDef([NotNull] SneakParser.ClassDefContext context)
        {
            var className = context.ID().GetText();
            var fields = VisitBody(context.body());
            var lineNo = context.Start.Line;
            var charNo = context.Start.Column + 1;

            var entity = new Entity(lineNo, charNo)
            {
                Name = className,
                Fields = (fields as Field[])?.ToList(),
                Annotations = VisitClassAnnotations(context.annotations())
            };

            if(!Entities.TryAdd(className, entity))
            {
                throw new ParsingException($"Duplicate declaration of {className}", entity);
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
            var props = context.property().Select(VisitProperty).OfType<Field>();

            return props.ToArray();
        }
        public override object VisitProperty([NotNull] SneakParser.PropertyContext context)
        {
            var lineNo = context.Start.Line;
            var charNo = context.Start.Column + 1;
            var property = new Field(lineNo, charNo)
            {
                Name = context.ID().GetText(),
                Annotations = VisitAnnotations(context.annotations()) as List<Annotation>,
                Type = VisitFieldType(context.TYPE())
            };

            return property;
        }

        public Type VisitFieldType(ITerminalNode type)
        {
            var lineNo = type.Symbol.Line;
            var charNo = type.Symbol.Column + 1;
            return _typeParser.ParseTypeName(type.GetText(), lineNo, charNo);
        }

        public override object VisitAnnotations([NotNull] SneakParser.AnnotationsContext context)
        {
            return context.annotation().Select(VisitAnnotation).OfType<Annotation>().ToList();
        }

        public List<Annotation> VisitClassAnnotations([NotNull] SneakParser.AnnotationsContext context)
        {
            return context.annotation().Select(VisitClassAnnotation).ToList();
        }

        public Annotation VisitClassAnnotation([NotNull] SneakParser.AnnotationContext context)
        {
            var annotation = VisitAnnotation(context) as Annotation;

            return annotation;
        }

        public override object VisitAnnotation([NotNull] SneakParser.AnnotationContext context)
        {
            var lineNo = context.Start.Line;
            var charNo = context.Start.Column + 1;
            return new Annotation(lineNo, charNo)
            {
                Name = context.ID().GetText(),
                Params = VisitParams(context.@params()) as List<AnnotationParam>
            };
        }

        public override object VisitParams([NotNull] SneakParser.ParamsContext context)
        {
            var paramlist = context.paramlist();
            if(paramlist != null)
                return VisitParamlist(context.paramlist());
            return new List<AnnotationParam>();
        }

        public override object VisitParamlist([NotNull] SneakParser.ParamlistContext context)
        {
            return context.param().Select(VisitParam).OfType<AnnotationParam>().ToList();
        }

        public override object VisitParam([NotNull] SneakParser.ParamContext context)
        {
            var lineNo = context.Start.Line;
            var charNo = context.Start.Column + 1;
            return _annotationParamParser.ParseAnnotationParam(context.ID().GetText(), context.VALUE().GetText(), lineNo, charNo);
        }
    }
}
