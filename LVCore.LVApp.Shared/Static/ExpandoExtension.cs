using System;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LVCore.LVApp.Shared.Static
{
    public static class ExpandoExtension
    {
        public static T ConvertTo<T>(this ExpandoObject expando) where T : new()
        {
            if (expando == null) throw new ArgumentNullException(nameof(expando));

            T obj = new T();
            var objType = typeof(T);

            foreach (var property in expando)
            {
                PropertyInfo propInfo = objType.GetProperty(property.Key);
                if (propInfo != null && propInfo.CanWrite)
                {
                    object value = property.Value;

                    // ✅ Null değerleri yönet (Nullable destekli)
                    if (value == null)
                    {
                        if (Nullable.GetUnderlyingType(propInfo.PropertyType) != null)
                        {
                            propInfo.SetValue(obj, null);
                            continue;
                        }
                        else if (propInfo.PropertyType.IsValueType)
                        {
                            value = Activator.CreateInstance(propInfo.PropertyType);
                        }
                    }

                    try
                    {
                        // 🛠 Koleksiyon ve ICollection<T> dönüşümü
                        if (typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType) && propInfo.PropertyType.IsGenericType)
                        {
                            Type elementType = propInfo.PropertyType.GetGenericArguments()[0];
                            var jsonList = JsonConvert.SerializeObject(value);
                            var convertedList = JsonConvert.DeserializeObject(jsonList, typeof(List<>).MakeGenericType(elementType));

                            if (convertedList != null)
                            {
                                // Eğer property ICollection<T> ise, List<T> olarak atıyoruz.
                                propInfo.SetValue(obj, convertedList);
                                continue;
                            }
                        }

                        // 🛠 Özel DateTime ve Nullable<DateTime> Dönüşümü
                        if (propInfo.PropertyType == typeof(DateTime) || propInfo.PropertyType == typeof(DateTime?))
                        {
                            if (value is string dateString)
                            {
                                DateTime parsedDate;
                                string[] formats = { "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy", "yyyy-MM-dd HH:mm:ss" };

                                if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                                {
                                    propInfo.SetValue(obj, parsedDate);
                                    continue;
                                }
                            }
                            else if (value is long timestamp)
                            {
                                var date = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
                                propInfo.SetValue(obj, date);
                                continue;
                            }
                            else if (value is DateTime dt)
                            {
                                propInfo.SetValue(obj, dt);
                                continue;
                            }
                        }

                        // 🛠 Genel Tür Dönüştürme (DateTime ve ICollection hariç)
                        Type targetType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                        object convertedValue = Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
                        propInfo.SetValue(obj, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidCastException($"'{property.Key}' alanı '{propInfo.PropertyType.Name}' türüne dönüştürülemedi. Gelen değer: {value}", ex);
                    }
                }
            }

            return obj;
        }
    }
}
