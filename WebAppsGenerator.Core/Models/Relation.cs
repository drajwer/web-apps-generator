namespace WebAppsGenerator.Core.Models
{
    /// <summary>
    /// Defines relation between entities
    /// </summary>
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
