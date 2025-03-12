using System;
using System.IO;
using System.Reflection;
using LVCore.LVAppService.Config;
using Newtonsoft.Json;

namespace LVCore.LVAppService.Proxies
{
    public class LVisionFormProxy
    {
        private readonly object _legacyFormInstance;
        private readonly Type _legacyFormType;

        public LVisionFormProxy(object formInstance, Type formType)
        {
            _legacyFormInstance = formInstance;
            _legacyFormType = formType;

            // JSON'dan verileri oku ve form nesnesine aktar
            LoadFromJson();
        }

        public object GetLegacyFormInstance() => _legacyFormInstance;

        private void LoadFromJson()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("config.json dosyası bulunamadı.");
                return;
            }

            try
            {
                string json = File.ReadAllText(jsonPath);
                var configData = JsonConvert.DeserializeObject<ConfigData>(json);

                if (configData?.FormParameters != null)
                {
                    var parameters = configData.FormParameters;

                    SetPropertyValue("ConnectionString", parameters.ConnectionString);
                    SetPropertyValue("LanguageID", parameters.LanguageID);
                    SetPropertyValue("DomainID", parameters.DomainID);
                    SetPropertyValue("LogisticSiteID", parameters.LogisticSiteID);
                    SetPropertyValue("UserName", parameters.UserName);
                    SetPropertyValue("UserID", parameters.UserID);
                    SetPropertyValue("DebugLevel", parameters.DebugLevel);
                    SetPropertyValue("CurrentID", parameters.CurrentID);
                    SetPropertyValue("MetricSystemID", parameters.MetricSystemID);
                    SetPropertyValue("ExecutablePath", parameters.ExecutablePath);
                    SetPropertyValue("DatasetPath", parameters.DatasetPath);
                    SetPropertyValue("SessionID", parameters.SessionID);
                    SetPropertyValue("ShowMaximized", parameters.ShowMaximized);
                    SetPropertyValue("DataBaseType", parameters.DataBaseType);
                    SetPropertyValue("DataBaseVersionMajor", parameters.DataBaseVersionMajor);
                    SetPropertyValue("DataBaseVersionMinor", parameters.DataBaseVersionMinor);
                    SetPropertyValue("MultipleIDs", parameters.MultipleIDs);
                    SetPropertyValue("Local", parameters.Local);
                    SetPropertyValue("RemoteMachine", parameters.RemoteMachine);
                    SetPropertyValue("RemoteTcpPort", parameters.RemoteTcpPort);
                    SetPropertyValue("DepositorAccess", parameters.DepositorAccess);
                    SetPropertyValue("LogisticSiteAccess", parameters.LogisticSiteAccess);
                    SetPropertyValue("LogisticUnitID", parameters.LogisticUnitID);
                    SetPropertyValue("EmployeeID", parameters.EmployeeID);
                    SetPropertyValue("MaxRecords", parameters.MaxRecords);
                    SetPropertyValue("isOverride", parameters.isOverride);

                    Console.WriteLine("Legacy form JSON'dan dolduruldu.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Legacy form JSON'dan doldurulurken hata oluştu: " + ex.Message);
            }
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            PropertyInfo property = _legacyFormType.GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(_legacyFormInstance, value);
            }
        }
    }
}
