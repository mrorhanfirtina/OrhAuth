namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_DashboardVisualization")]
    public class DashboardVisualization
    {
        [Column("dbv_ID", true)] // Primary Key
        public int dbv_ID { get; set; }

        [Column("dbv_DashboardID")]
        public int? dbv_DashboardID { get; set; }

        [Column("dbv_VisualizationID")]
        public int? dbv_VisualizationID { get; set; }

        [Column("dbv_AutoRefreshLED")]
        public int? dbv_AutoRefreshLED { get; set; }

        [Column("dbv_RefreshRate")]
        public int? dbv_RefreshRate { get; set; }

        [Column("dbv_DomainID")]
        public int? dbv_DomainID { get; set; }

    }
}