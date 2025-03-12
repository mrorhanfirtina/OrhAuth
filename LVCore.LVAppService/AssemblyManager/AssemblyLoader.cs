using LVCore.LVAppService.Config;
using LVCore.LVAppService.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LVCore.LVAppService.AssemblyManager
{
    public static class AssemblyLoader
    {
        private static Dictionary<string, string> ReferencePaths = new Dictionary<string, string>(); // Referans yolları
        private static readonly HashSet<string> LoadedAssemblies = new HashSet<string>(); // Yüklenen DLL'leri takip etmek için


        static AssemblyLoader()
        {
            LoadReferencePaths(); // Config dosyasından referans yollarını yükle
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }

        private static void LoadReferencePaths()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (File.Exists(jsonPath))
            {
                try
                {
                    string json = File.ReadAllText(jsonPath);
                    var configData = JsonConvert.DeserializeObject<ConfigData>(json);

                    if (configData?.ReferencePaths != null)
                    {
                        ReferencePaths = configData.ReferencePaths;
                        Console.WriteLine("DLL referans yolları yüklendi.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Config dosyası okunurken hata oluştu: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Config dosyası bulunamadı, varsayılan yollar kullanılacak.");
            }
        }


        public static void LoadAssemblies(params LVLibrary[] dllsToLoad)
        {
            if (ReferencePaths.Count == 0)
            {
                Console.WriteLine("Hiçbir referans yolu bulunamadı, DLL yüklenemiyor.");
                return;
            }

            foreach (LVLibrary dll in dllsToLoad)
            {
                string dllName = dll.ToString().Replace('_', '.') + ".dll";

                bool found = false;

                foreach (var referencePath in ReferencePaths.Values) // Tüm referans yollarında ara
                {
                    string dllPath = Path.Combine(referencePath, dllName);

                    if (File.Exists(dllPath) && !LoadedAssemblies.Contains(dllPath))
                    {
                        try
                        {
                            Assembly.LoadFrom(dllPath);
                            LoadedAssemblies.Add(dllPath);
                            Console.WriteLine($"Yüklendi: {dllPath}");
                            found = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"DLL yüklenirken hata oluştu: {dllPath}, {ex.Message}");
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"DLL bulunamadı: {dllName}");
                }
            }
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            string assemblyName = new AssemblyName(args.Name).Name + ".dll";

            foreach (var referencePath in ReferencePaths.Values)
            {
                string assemblyPath = Path.Combine(referencePath, assemblyName);

                if (File.Exists(assemblyPath))
                {
                    return Assembly.LoadFrom(assemblyPath);
                }
            }

            return null;
        }
    }
}
