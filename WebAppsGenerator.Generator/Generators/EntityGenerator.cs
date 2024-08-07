﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generator.Extensions;
using WebAppsGenerator.Generator.Models;
using Type = System.Type;

namespace WebAppsGenerator.Generator.Generators
{
    public class EntityGenerator
    {
        private readonly List<Type> _registerTypes;

        public EntityGenerator()
        {
            _registerTypes = new List<Type>();
        }

        public string GenerateEntities(IEnumerable<Entity> entities)
        {
            var entityList = entities.ToList();
            var entity = entityList[0];
            var templateTxt = GetTemplate();
            RegisterTypesForObject(typeof(Entity));

            var template = Template.Parse(templateTxt); // Parses and compiles the template

            var generatedEntity = template.Render(Hash.FromAnonymousObject(new { entity }));
            Console.Write(generatedEntity);
            return null;
        }

        private static string GetTemplate()
        {
            var assembly = Assembly.GetCallingAssembly();//.GetAssembly(typeof(Generator));//GetExecutingAssembly();
            var resourceName = $"WebAppsGenerator.Generator.Templates.Entity.liquid";
            var r = assembly.GetManifestResourceNames();

            using (var reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
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