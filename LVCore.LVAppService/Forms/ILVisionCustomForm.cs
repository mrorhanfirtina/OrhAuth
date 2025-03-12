namespace LVCore.LVAppService.Forms
{
    public interface ILVisionCustomForm
    {
            string ConnectionString { get; set; }
            int LanguageID { get; set; }
            int DomainID { get; set; }
            int LogisticSiteID { get; set; }
            string UserName { get; set; }
            int UserID { get; set; }
            int DebugLevel { get; set; }
            int CurrentID { get; set; }
            int MetricSystemID { get; set; }
            string ExecutablePath { get; set; }
            string DatasetPath { get; set; }
            int SessionID { get; set; }
            bool ShowMaximized { get; set; }
            int DataBaseType { get; set; }
            int DataBaseVersionMajor { get; set; }
            int DataBaseVersionMinor { get; set; }
            int[] MultipleIDs { get; set; }
            bool Local { get; set; }
            string RemoteMachine { get; set; }
            string RemoteTcpPort { get; set; }
            int[] DepositorAccess { get; set; }
            int[] LogisticSiteAccess { get; set; }
            int LogisticUnitID { get; set; }
            int EmployeeID { get; set; }
            int MaxRecords { get; set; }
            bool isOverride { get; set; }
            void AssignPropertiesToForm(ILVisionCustomForm destination, ILVisionCustomForm source);
    }
}
