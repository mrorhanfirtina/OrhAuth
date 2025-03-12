using LVCore.LVAppService.Config;
using Newtonsoft.Json;
using System;
using System.IO;

namespace LVCore.LVAppService.Forms
{
    public class LVisionBaseForm : ILVisionCustomForm
    {
        public string ConnectionString { get; set; }
        public int LanguageID { get; set; }
        public int DomainID { get; set; }
        public int LogisticSiteID { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public int DebugLevel { get; set; }
        public int CurrentID { get; set; }
        public int MetricSystemID { get; set; }
        public string ExecutablePath { get; set; }
        public string DatasetPath { get; set; }
        public int SessionID { get; set; }
        public bool ShowMaximized { get; set; }
        public int DataBaseType { get; set; }
        public int DataBaseVersionMajor { get; set; }
        public int DataBaseVersionMinor { get; set; }
        public int[] MultipleIDs { get; set; }
        public bool Local { get; set; }
        public string RemoteMachine { get; set; }
        public string RemoteTcpPort { get; set; }
        public int[] DepositorAccess { get; set; }
        public int[] LogisticSiteAccess { get; set; }
        public int LogisticUnitID { get; set; }
        public int EmployeeID { get; set; }
        public int MaxRecords { get; set; }
        public bool isOverride { get; set; }

        // Constructor �a�r�ld���nda JSON dosyas�ndan verileri y�kle
        public LVisionBaseForm()
        {
            LoadFromJson();
        }

        private void LoadFromJson()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (File.Exists(jsonPath))
            {
                try
                {
                    string json = File.ReadAllText(jsonPath);
                    var configData = JsonConvert.DeserializeObject<ConfigData>(json);

                    if (configData?.FormParameters != null)
                    {
                        var parameters = configData.FormParameters;

                        // T�m �zellikleri `FormParameters` nesnesinden doldur
                        ConnectionString = parameters.ConnectionString;
                        LanguageID = parameters.LanguageID;
                        DomainID = parameters.DomainID;
                        LogisticSiteID = parameters.LogisticSiteID;
                        UserName = parameters.UserName;
                        UserID = parameters.UserID;
                        DebugLevel = parameters.DebugLevel;
                        CurrentID = parameters.CurrentID;
                        MetricSystemID = parameters.MetricSystemID;
                        ExecutablePath = parameters.ExecutablePath;
                        DatasetPath = parameters.DatasetPath;
                        SessionID = parameters.SessionID;
                        ShowMaximized = parameters.ShowMaximized;
                        DataBaseType = parameters.DataBaseType;
                        DataBaseVersionMajor = parameters.DataBaseVersionMajor;
                        DataBaseVersionMinor = parameters.DataBaseVersionMinor;
                        MultipleIDs = parameters.MultipleIDs;
                        Local = parameters.Local;
                        RemoteMachine = parameters.RemoteMachine;
                        RemoteTcpPort = parameters.RemoteTcpPort;
                        DepositorAccess = parameters.DepositorAccess;
                        LogisticSiteAccess = parameters.LogisticSiteAccess;
                        LogisticUnitID = parameters.LogisticUnitID;
                        EmployeeID = parameters.EmployeeID;
                        MaxRecords = parameters.MaxRecords;
                        isOverride = parameters.isOverride;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("JSON dosyas� okunurken hata olu�tu: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("config.json dosyas� bulunamad�, varsay�lan de�erler kullan�lacak.");
            }
        }

        public void AssignPropertiesToForm(ILVisionCustomForm destination, ILVisionCustomForm source)
        {
            if (destination == null || source == null)
                throw new ArgumentNullException();

            destination.ConnectionString = source.ConnectionString;
            destination.LanguageID = source.LanguageID;
            destination.DomainID = source.DomainID;
            destination.LogisticSiteID = source.LogisticSiteID;
            destination.UserName = source.UserName;
            destination.UserID = source.UserID;
            destination.DebugLevel = source.DebugLevel;
            destination.CurrentID = source.CurrentID;
            destination.MetricSystemID = source.MetricSystemID;
            destination.ExecutablePath = source.ExecutablePath;
            destination.DatasetPath = source.DatasetPath;
            destination.SessionID = source.SessionID;
            destination.ShowMaximized = source.ShowMaximized;
            destination.DataBaseType = source.DataBaseType;
            destination.DataBaseVersionMajor = source.DataBaseVersionMajor;
            destination.DataBaseVersionMinor = source.DataBaseVersionMinor;
            destination.MultipleIDs = source.MultipleIDs;
            destination.Local = source.Local;
            destination.RemoteMachine = source.RemoteMachine;
            destination.RemoteTcpPort = source.RemoteTcpPort;
            destination.DepositorAccess = source.DepositorAccess;
            destination.LogisticSiteAccess = source.LogisticSiteAccess;
            destination.LogisticUnitID = source.LogisticUnitID;
            destination.EmployeeID = source.EmployeeID;
            destination.MaxRecords = source.MaxRecords;
            destination.isOverride = source.isOverride;
        }
    }
}
