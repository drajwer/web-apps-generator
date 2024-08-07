﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using PluralizationService;
using PluralizationService.English;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Provides pluralization and singularization of English words
    /// </summary>
    public class PluralizationHelper
    {
        private static readonly IPluralizationApi Api;
        private static readonly CultureInfo CultureInfo;

        static PluralizationHelper()
        {
            var builder = new PluralizationApiBuilder();
            builder.AddEnglishProvider();

            Api = builder.Build();
            CultureInfo = new CultureInfo("en-US");
        }


        public static string Pluralize(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "";

            var pluralizedName = Api.Pluralize(name, CultureInfo) ?? name;

            if (name == pluralizedName)
                return name + "es"; 

            return pluralizedName;
        }

        public static string Singularize(string name)
        {
            return Api.Singularize(name, CultureInfo) ?? name;
        }
    }
}
