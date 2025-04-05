using OrhAuth.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using OrhAuth.Exceptions;

namespace OrhAuth.Data.Context
{
    /// <summary>
    /// Caches metadata for extended user properties marked with <see cref="ExtendUserAttribute"/>.
    /// Helps dynamically generate database schema and manage extended user fields.
    /// </summary>
    public static class SchemaMetadataCache
    {
        private static readonly Dictionary<Type, List<PropertyMetadata>> _cache =
            new Dictionary<Type, List<PropertyMetadata>>();

        private static readonly object _lockObject = new object();
        private static Type _extendedUserType;


        /// <summary>
        /// Registers the extended user type and caches its properties decorated with <see cref="ExtendUserAttribute"/>.
        /// </summary>
        /// <param name="extendedType">The extended user class type.</param>
        /// <exception cref="ArgumentNullException">Thrown if the type is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no valid extended properties are found.</exception>
        public static void RegisterExtendedType(Type extendedType)
        {
            try
            {
                if (extendedType == null)
                    throw new ArgumentNullException(nameof(extendedType), "The extended user type cannot be null. You must provide a valid type that inherits from User.");


                _extendedUserType = extendedType;

                lock (_lockObject)
                {
                    if (_cache.ContainsKey(typeof(Models.Entities.User)))
                    {
                        _cache.Remove(typeof(Models.Entities.User));
                    }

                    var properties = extendedType.GetProperties()
                        .Where(p => p.IsDefined(typeof(ExtendUserAttribute), false))
                        .ToList();

                    if (properties.Count == 0)
                        throw new OrhAuthException($"No properties marked with [ExtendUser] were found in the extended user type '{extendedType.FullName}'. Please ensure that at least one property is annotated with [ExtendUserAttribute].");


                    var propertyMetadataList = properties.Select(p =>
                    {
                        try
                        {
                            return new PropertyMetadata
                            {
                                Property = p,
                                Attribute = (ExtendUserAttribute)p.GetCustomAttribute(typeof(ExtendUserAttribute))
                            };
                        }
                        catch (Exception ex)
                        {
                            throw new OrhAuthException($"Failed to retrieve the ExtendUserAttribute for property '{p.Name}': {ex.Message}", ex);
                        }
                    }).ToList();

                    _cache[typeof(Models.Entities.User)] = propertyMetadataList;
                }
            }
            catch (Exception ex)
            {
                throw new OrhAuthException($"An error occurred while registering the extended user type: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the registered extended user type.
        /// </summary>
        /// <returns>The extended <c>User</c> type, or <c>null</c> if not set.</returns>
        public static Type GetExtendedUserType()
        {
            return _extendedUserType;
        }


        /// <summary>
        /// Retrieves metadata for all properties of the specified entity type that are marked with <see cref="ExtendUserAttribute"/>.
        /// </summary>
        /// <param name="entityType">The entity type to scan for extended properties.</param>
        /// <returns>A list of <see cref="PropertyMetadata"/> containing property and attribute info.</returns>
        public static List<PropertyMetadata> GetExtendedProperties(Type entityType)
        {
            if (_cache.ContainsKey(entityType))
            {
                return _cache[entityType];
            }

            lock (_lockObject)
            {
                if (_cache.ContainsKey(entityType))
                {
                    return _cache[entityType];
                }

                var properties = entityType.GetProperties()
                    .Where(p => p.IsDefined(typeof(ExtendUserAttribute), false))
                    .Select(p => new PropertyMetadata
                    {
                        Property = p,
                        Attribute = (ExtendUserAttribute)p.GetCustomAttribute(typeof(ExtendUserAttribute))
                    })
                    .ToList();

                _cache[entityType] = properties;
                return properties;
            }
        }
    }


    /// <summary>
    /// Holds metadata for an extended property, including its reflection info and the associated attribute.
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>
        /// The <see cref="PropertyInfo"/> for the extended property.
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// The associated <see cref="ExtendUserAttribute"/> for the property.
        /// </summary>
        public ExtendUserAttribute Attribute { get; set; }
    }
}