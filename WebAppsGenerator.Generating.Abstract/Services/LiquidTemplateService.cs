using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using DotLiquid.NamingConventions;
using WebAppsGenerator.Core.Extensions;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class LiquidTemplateService
    {
        private readonly List<Type> _registerTypes;

        static LiquidTemplateService()
        {
            Template.NamingConvention = new CSharpNamingConvention();
        }

        public LiquidTemplateService()
        {
            _registerTypes = new List<Type>();
        }

        public IEnumerable<string> RenderTemplate(string template, IEnumerable<Drop> sources)
        {
            RegisterTypesForObject(sources.GetType());
            var parseTemplate = Template.Parse(template); // Parses and compiles the template
            foreach (var source in sources)
            {
                yield return parseTemplate.Render(Hash.FromAnonymousObject(new { Params = source }));
            }
        }

        public string RenderTemplate(string template, Drop source)
        {
            RegisterTypesForObject(source.GetType());
            var parseTemplate = Template.Parse(template); // Parses and compiles the template

            return parseTemplate.Render(Hash.FromAnonymousObject(new {Params = source}));
        }

        public string RenderTemplate(string template, object source)
        {
            RegisterTypesForObject(source.GetType());
            var parseTemplate = Template.Parse(template); // Parses and compiles the template

            return parseTemplate.Render(Hash.FromAnonymousObject(source));
        }

        private void RegisterTypesForObject(Type t)
        {
            if (t.IsSimpleType() || IsRegisteredType(t))
                return;
            RegisterType(t);

            foreach (var prop in t.GetProperties())
            {
                var propType = prop.PropertyType;
                var underlyingType = Nullable.GetUnderlyingType(propType);

                if (underlyingType != null)
                    RegisterTypesForNullable(t, propType, underlyingType);

                else if (propType.IsArray)
                    RegisterTypesForObject(propType.GetElementType());

                else if (propType.IsGenericType && (typeof(IEnumerable).IsAssignableFrom(propType)))
                    RegisterGenericCollection(propType);

                else if (propType.IsEnum)
                    RegisterEnum(propType);

                else if (!propType.IsSimpleType())
                    RegisterTypesForObject(propType);
            }
        }

        private void RegisterGenericCollection(Type propType)
        {
            var elemType = propType.GenericTypeArguments.FirstOrDefault();
            if (elemType != null)
                RegisterTypesForObject(elemType);
        }

        private void RegisterEnum(Type propType)
        {
            Template.RegisterSafeType(propType, c => c.ToString());
        }

        private void RegisterTypesForNullable(Type t, Type propType, Type underlyingType)
        {
            if (underlyingType.IsEnum)
            {
                Template.RegisterSafeType(propType,
                    c => c == null
                        ? ""
                        : c.ToString());
                RegisterEnum(underlyingType);
            }
            else
            {
                Template.RegisterSafeType(propType, c => c == null ? "" : propType.ToString());
                RegisterTypesForObject(t);
            }
        }

        private void RegisterType(Type t)
        {
            Template.RegisterSafeType(t, t.GetProperties().Select(prop => prop.Name).ToArray());
            _registerTypes.Add(t);
        }

        private bool IsRegisteredType(Type t)
        {
            return _registerTypes.Contains(t);
        }
    }
}
