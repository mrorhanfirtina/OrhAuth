using System.Reflection;
using System;
using System.Linq;

namespace LVCore.LVAppService.AssemblyManager
{
    public static class DynamicFactory
    {
        public static T CreateInstance<T>(string assemblyName, string className, params object[] constructorArgs)
        {
            try
            {
                // Yüklenmiş assembly'yi bul
                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.GetName().Name == assemblyName);

                if (assembly == null) 
                    throw new ArgumentException($"Assembly bulunamadı: {assemblyName}");

                // Sınıfı bul
                Type type = assembly.GetType(className);
                if (type == null) 
                    throw new ArgumentException($"Sınıf bulunamadı: {className}");

                // Interface uyumluluğunu kontrol et
                if (!typeof(T).IsAssignableFrom(type))
                    throw new ArgumentException($"{className} sınıfı {typeof(T).Name} interface'ini implement etmiyor.");

                // Sınıfı oluştur ve interface olarak cast et
                return (T)Activator.CreateInstance(type, constructorArgs);
            }
            catch (Exception ex)
            {
                throw new Exception($"{assemblyName} assembly'sinden {className} sınıfı oluşturulurken hata: {ex.Message}", ex);
            }
        }

        public static object CreateInstance(string assemblyName, string className, params object[] constructorArgs)
        {
            try
            {
                // Yüklenmiş assembly'yi bul
                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.GetName().Name == assemblyName);

                if (assembly == null)
                    throw new ArgumentException($"Assembly bulunamadı: {assemblyName}");

                // Sınıfı bul
                Type type = assembly.GetType(className);
                if (type == null)
                    throw new ArgumentException($"Sınıf bulunamadı: {className}");

                // Sınıfı oluştur
                return Activator.CreateInstance(type, constructorArgs);
            }
            catch (Exception ex)
            {
                throw new Exception($"{assemblyName} assembly'sinden {className} sınıfı oluşturulurken hata: {ex.Message}", ex);
            }
        }
    }
}
