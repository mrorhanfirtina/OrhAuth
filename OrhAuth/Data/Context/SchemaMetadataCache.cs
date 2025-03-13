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
        private static Type _extendedUserType;

        // RegisterExtendedType metodunu güncelle
        public static void RegisterExtendedType(Type extendedType)
        {
            try
            {
                if (extendedType == null)
                    throw new ArgumentNullException(nameof(extendedType), "Genişletilmiş tip null olamaz!");

                // Genişletilmiş tipi sakla
                _extendedUserType = extendedType;

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
                        .ToList();

                    if (properties.Count == 0)
                        throw new InvalidOperationException($"Genişletilmiş tipte ({extendedType.FullName}) ExtendUserAttribute ile işaretlenmiş özellik bulunamadı!");

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
                            throw new InvalidOperationException($"{p.Name} özelliği için attribute alınamadı: {ex.Message}", ex);
                        }
                    }).ToList();

                    _cache[typeof(Models.Entities.User)] = propertyMetadataList;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RegisterExtendedType hatası: {ex.Message}");
                throw; // Hatayı tekrar fırlat
            }
        }

        // Genişletilmiş User tipini almak için yeni metot ekle
        public static Type GetExtendedUserType()
        {
            return _extendedUserType;
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

    // PropertyMetadata sınıfı
    public class PropertyMetadata
    {
        public PropertyInfo Property { get; set; }
        public ExtendUserAttribute Attribute { get; set; }
    }
}