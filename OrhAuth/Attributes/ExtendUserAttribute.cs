using System;

namespace OrhAuth.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExtendUserAttribute : Attribute
    {
        public int MaxLength { get; }
        public bool IsRequired { get; }
        public bool IsUnique { get; }
        public string DefaultValue { get; }
        public string Description { get; }
        public int Order { get; }
        public string DbType { get; } // Örneğin: "nvarchar", "decimal", "datetime"

        public ExtendUserAttribute(
            int maxLength = 255,
            bool isRequired = false,
            bool isUnique = false,
            string defaultValue = null,
            string description = null,
            int order = 0,
            string dbType = null)
        {
            MaxLength = maxLength;
            IsRequired = isRequired;
            IsUnique = isUnique;
            DefaultValue = defaultValue;
            Description = description;
            Order = order;
            DbType = dbType;
        }
    }
}