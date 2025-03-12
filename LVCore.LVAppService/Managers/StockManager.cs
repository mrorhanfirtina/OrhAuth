using LVCore.LVAppService.AssemblyManager;
using LVCore.LVAppService.Enums;
using LVCore.LVAppService.Proxies;
using LVCore.LVAppService.Services;
using System;
using System.Data;
using System.Reflection;

namespace LVCore.LVAppService.Managers
{
    public class StockManager
    {
        private readonly LVisionFormProxy _legacyFormProxy;
        private readonly IStockService _legacyInstance;

        public StockManager()
        {
            // DLL'leri yükle
            AssemblyLoader.LoadAssemblies(LVLibrary.Mantis_LVision_Proxy, LVLibrary.Mantis_LVision_Win32, LVLibrary.Mantis_LVision_Interfaces);

            Type legacyFormType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.Contains("Mantis.LVision.Win32"))
                {
                    Console.WriteLine($"✅ Assembly Yüklendi: {assembly.FullName}");

                    try
                    {
                        // Sadece LVBasicForm tipini bul, tüm tipleri listeleme
                        legacyFormType = assembly.GetType("Mantis.LVision.Win32.LVBasicForm", false);
                        if (legacyFormType != null)
                        {
                            Console.WriteLine("LVBasicForm sınıfı bulundu.");
                            break;
                        }
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        // Hata detaylarını logla ama devam et
                        Console.WriteLine($"Assembly yüklenirken bazı tipler yüklenemedi: {ex.Message}");
                        foreach (Exception loaderException in ex.LoaderExceptions)
                        {
                            Console.WriteLine($"Loader Exception: {loaderException.Message}");
                        }
                    }
                }
            }

            if (legacyFormType == null)
                throw new Exception("LVBasicForm sınıfı bulunamadı.");

            try
            {
                object legacyFormInstance = Activator.CreateInstance(legacyFormType);
                _legacyFormProxy = new LVisionFormProxy(legacyFormInstance, legacyFormType);
            }
            catch (Exception ex)
            {
                throw new Exception("LVBasicForm instance oluşturulurken hata: " + ex.Message);
            }

            _legacyInstance = new StockServiceProxy(); 
        }

        public DataSet OpenStockbyID(int id)
        {
             DataSet t = _legacyInstance.OpenStockbyID(id, _legacyFormProxy.GetLegacyFormInstance());

            var i = 1;

            return t;
        }


    }
}