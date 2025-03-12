namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("COM_Dashboard")]
    public class Dashboard
    {
        [Column("dbd_ID", true)] // Primary Key
        public int dbd_ID { get; set; }

        [Column("dbd_Code")]
        public string dbd_Code { get; set; }

        [Column("dbd_VisualCategoryID")]
        public int? dbd_VisualCategoryID { get; set; }

        [Column("dbd_AutoRefreshLED")]
        public int? dbd_AutoRefreshLED { get; set; }

        [Column("dbd_RefreshRate")]
        public int? dbd_RefreshRate { get; set; }

        [Column("dbd_DatasourceTimeOut")]
        public int? dbd_DatasourceTimeOut { get; set; }

        [Column("dbd_Theme")]
        public string dbd_Theme { get; set; }

        [Column("dbd_DomainID")]
        public int? dbd_DomainID { get; set; }

        [Column("dbd_SizeID")]
        public int? dbd_SizeID { get; set; }

        [Column("dbd_LastUpdateTime")]
        public DateTime? dbd_LastUpdateTime { get; set; }

        [Column("dbd_Layout")]
        public string dbd_Layout { get; set; }

        [Column("dbd_DisplayOrder")]
        public int? dbd_DisplayOrder { get; set; }

        [Column("dbd_AllLSLED")]
        public int? dbd_AllLSLED { get; set; }

    }
}