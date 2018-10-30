﻿namespace WebAppsGenerator.Core.Models
{
    public class AnnotationParam
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; } // TODO: discuss if string type will be better
    }
}