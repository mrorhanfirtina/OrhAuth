using System;

namespace OrhAuth.Attributes
{
    /// <summary>
    /// Specifies that a property is part of the extended user model in OrhAuth.
    /// This attribute allows additional metadata to be defined for the database schema
    /// such as length, uniqueness, default value, and custom database type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExtendUserAttribute : Attribute
    {
        /// <summary>
        /// Maximum allowed length of the property in the database.
        /// Default is 255.
        /// </summary>
        public int MaxLength { get; }

        /// <summary>
        /// Indicates whether the property is required (not null).
        /// </summary>
        public bool IsRequired { get; }

        /// <summary>
        /// Indicates whether the property value must be unique in the database.
        /// </summary>
        public bool IsUnique { get; }

        /// <summary>
        /// The default value to be used when the field is not provided.
        /// </summary>
        public string DefaultValue { get; }

        /// <summary>
        /// Description of the property, typically used for documentation or metadata.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The order in which the property should appear in UI or form generation.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Custom database type for the property.
        /// For example: "nvarchar", "decimal", "datetime".
        /// </summary>
        public string DbType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendUserAttribute"/> class with optional configuration parameters.
        /// </summary>
        /// <param name="maxLength">Maximum length of the field. Default is 255.</param>
        /// <param name="isRequired">Whether the field is required.</param>
        /// <param name="isUnique">Whether the field must be unique.</param>
        /// <param name="defaultValue">Default value of the field.</param>
        /// <param name="description">Description of the field.</param>
        /// <param name="order">Display or processing order.</param>
        /// <param name="dbType">Database column type (e.g., nvarchar, int).</param>
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
