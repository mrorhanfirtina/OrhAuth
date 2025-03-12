using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LVCore.LVApp.Shared.Config
{
    public static class AppConfigManager
    {
        private static readonly string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        private static ConfigData _configData;

        // Konfigürasyon verisini yükle
        static AppConfigManager()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            if (File.Exists(_jsonPath))
            {
                try
                {
                    string json = File.ReadAllText(_jsonPath);
                    _configData = JsonConvert.DeserializeObject<ConfigData>(json) ?? new ConfigData();
                    Console.WriteLine("✅ Config dosyası başarıyla yüklendi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Config dosyası okunurken hata oluştu: {ex.Message}");
                    _configData = new ConfigData(); // Varsayılan boş nesne
                }
            }
            else
            {
                Console.WriteLine("⚠️ Config dosyası bulunamadı, varsayılan değerler kullanılacak.");
                _configData = new ConfigData(); // Varsayılan boş nesne
            }
        }

        public static string GetConnectionString()
        {
            return _configData?.FormParameters?.ConnectionString ?? throw new InvalidOperationException("ConnectionString bulunamadı.");
        }
    }

    public class ConfigData
    {
        [JsonProperty("FormParameters")]
        public FormParameters FormParameters { get; set; }

        [JsonProperty("ReferencePaths")]
        public Dictionary<string, string> ReferencePaths { get; set; }
    }

    public class FormParameters
    {
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("LanguageID")]
        public int LanguageID { get; set; }

        [JsonProperty("DomainID")]
        public int DomainID { get; set; }

        [JsonProperty("LogisticSiteID")]
        public int LogisticSiteID { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("UserID")]
        public int UserID { get; set; }

        [JsonProperty("DebugLevel")]
        public int DebugLevel { get; set; }

        [JsonProperty("CurrentID")]
        public int CurrentID { get; set; }

        [JsonProperty("MetricSystemID")]
        public int MetricSystemID { get; set; }

        [JsonProperty("ExecutablePath")]
        public string ExecutablePath { get; set; }

        [JsonProperty("DatasetPath")]
        public string DatasetPath { get; set; }

        [JsonProperty("SessionID")]
        public int SessionID { get; set; }

        [JsonProperty("ShowMaximized")]
        public bool ShowMaximized { get; set; }

        [JsonProperty("DataBaseType")]
        public int DataBaseType { get; set; }

        [JsonProperty("DataBaseVersionMajor")]
        public int DataBaseVersionMajor { get; set; }

        [JsonProperty("DataBaseVersionMinor")]
        public int DataBaseVersionMinor { get; set; }

        [JsonProperty("MultipleIDs")]
        public int[] MultipleIDs { get; set; }

        [JsonProperty("Local")]
        public bool Local { get; set; }

        [JsonProperty("RemoteMachine")]
        public string RemoteMachine { get; set; }

        [JsonProperty("RemoteTcpPort")]
        public string RemoteTcpPort { get; set; }

        [JsonProperty("DepositorAccess")]
        public int[] DepositorAccess { get; set; }

        [JsonProperty("LogisticSiteAccess")]
        public int[] LogisticSiteAccess { get; set; }

        [JsonProperty("LogisticUnitID")]
        public int LogisticUnitID { get; set; }

        [JsonProperty("EmployeeID")]
        public int EmployeeID { get; set; }

        [JsonProperty("MaxRecords")]
        public int MaxRecords { get; set; }

        [JsonProperty("isOverride")]
        public bool isOverride { get; set; }
    }
}
