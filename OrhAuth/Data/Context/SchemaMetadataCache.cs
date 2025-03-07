using OrhAuth.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace OrhAuth.Data.Context
{
    /// <summary>
    /// Entity tipleri için şema metadata önbelleği
    /// </summary>
    public static class SchemaMetadataCache
    {
        private static readonly Dictionary<Type, List<PropertyMetadata>> _cache =
            new Dictionary<Type, List<PropertyMetadata>>();

        private static readonly object _lockObject = new object();

        /// <summary> 
        /// Bir tip için genişletilmiş özellikleri döndürür
        /// </summary>
        public static List<PropertyMetadata> GetExtendedProperties(Type entityType)
        {
            // Önbellekte varsa doğrudan döndür
            if (_cache.ContainsKey(entityType))
            {
                return _cache[entityType];
            }

            // Değilse, thread güvenli şekilde oluştur
            lock (_lockObject)
            {
                // Kilitlendikten sonra tekrar kontrol et (double-check locking)
                if (_cache.ContainsKey(entityType))
                {
                    return _cache[entityType];
                }

                // Genişletilmiş özellikleri bul
                var properties = entityType.GetProperties()
                    .Where(p => p.IsDefined(typeof(ExtendUserAttribute), false))
                    .Select(p => new PropertyMetadata
                    {
                        Property = p,
                        Attribute = (ExtendUserAttribute)p.GetCustomAttribute(typeof(ExtendUserAttribute))
                    })
                    .ToList();

                // Önbelleğe ekle
                _cache[entityType] = properties;
                return properties;
            }
        }

        /// <summary>
        /// Önbelleği temizler
        /// </summary>
        public static void ClearCache()
        {
            lock (_lockObject)
            {
                _cache.Clear();
            }
        }
    }

    /// <summary>
    /// Genişletilmiş özellik metadata'sı
    /// </summary>
    public class PropertyMetadata
    {
        public PropertyInfo Property { get; set; }
        public ExtendUserAttribute Attribute { get; set; }
    }
}
