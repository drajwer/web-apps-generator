using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Models
{
    public class Relation
    {
        public bool HasOne { get; set; }
        public bool HasMany => !HasOne;
        public bool WithOne { get; set; }
        public bool WithMany => !WithOne;
        public bool Primary { get; set; }
        public string SecondFieldName { get; set; }
    }
}
