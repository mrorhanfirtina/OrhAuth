using OrhAuth.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace OrhAuth.Data.Context
{
    public static class SchemaMetadataCache
    {
        private static readonly Dictionary<Type, List<PropertyMetadata>> _cache =
            new Dictionary<Type, List<PropertyMetadata>>();

        private static readonly object _lockObject = new object();

        public static void RegisterExtendedType(Type extendedType)
        {
            if (extendedType == null)
                return;

            lock (_lockObject)
            {
                // Önbelleği temizle
                if (_cache.ContainsKey(typeof(Models.Entities.User)))
                {
                    _cache.Remove(typeof(Models.Entities.User));
                }

                // Genişletilmiş özellikleri bul ve önbelleğe ekle
                var properties = extendedType.GetProperties()
                    .Where(p => p.IsDefined(typeof(ExtendUserAttribute), false))
                    .Select(p => new PropertyMetadata
                    {
                        Property = p,
                        Attribute = (ExtendUserAttribute)p.GetCustomAttribute(typeof(ExtendUserAttribute))
                    })
                    .ToList();

                _cache[typeof(Models.Entities.User)] = properties;
            }
        }

        // Mevcut GetExtendedProperties metodu aynen kalacak
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

    // PropertyMetadata sınıfı eğer yoksa ekleyin
    public class PropertyMetadata
    {
        public PropertyInfo Property { get; set; }
        public ExtendUserAttribute Attribute { get; set; }
    }
}
