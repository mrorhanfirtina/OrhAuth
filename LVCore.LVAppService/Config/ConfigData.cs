using System.Collections.Generic;

namespace LVCore.LVAppService.Config
{
    public class ConfigData
    {
        public FormParameters FormParameters { get; set; }
        public Dictionary<string, string> ReferencePaths { get; set; }
    }

    public class FormParameters
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
    }
}
