using LVCore.LVAppService.Config;
using LVCore.LVAppService.Forms;
using LVCore.LVAppService.Services;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace LVCore.LVAppService.Proxies
{
    public class StockServiceProxy : IStockService
    {
        private readonly object _wsStockInstance;
        private readonly Type _wsStockType;

        public StockServiceProxy()
        {
            string dllPath = GetDllPath("Mantis.LVision.Proxy.dll");

            if (!File.Exists(dllPath))
                throw new FileNotFoundException($"DLL bulunamadı: {dllPath}");

            Assembly assembly = Assembly.LoadFrom(dllPath);
            _wsStockType = assembly.GetType("Mantis.LVision.Proxy.WsStock");

            if (_wsStockType == null)
                throw new Exception("WsStock sınıfı bulunamadı.");

            _wsStockInstance = Activator.CreateInstance(_wsStockType);
        }

        private string GetDllPath(string dllName)
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (!File.Exists(configPath))
                throw new FileNotFoundException("config.json dosyası bulunamadı.");

            string json = File.ReadAllText(configPath);
            var configData = JsonConvert.DeserializeObject<ConfigData>(json);

            if (configData?.ReferencePaths == null || configData.ReferencePaths.Count == 0)
                throw new Exception("ReferencePaths bulunamadı.");

            // İlk uygun yolu al
            foreach (var path in configData.ReferencePaths.Values)
            {
                string fullPath = Path.Combine(path, dllName);
                if (File.Exists(fullPath))
                    return fullPath;
            }

            throw new FileNotFoundException($"DLL {dllName} belirtilen ReferencePaths içinde bulunamadı.");
        }


        public DataSet OpenStockbyID(int stockId, object form)
        {
            MethodInfo method = _wsStockType.GetMethod("OpenStockbyID");

            if (method == null)
                throw new Exception("OpenStockbyID metodu bulunamadı.");

            return (DataSet)method.Invoke(_wsStockInstance, new object[] { stockId, form });
        }

    }
}
