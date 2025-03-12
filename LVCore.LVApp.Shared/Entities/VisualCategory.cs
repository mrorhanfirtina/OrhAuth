namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_VisualCategory")]
    public class VisualCategory
    {
        [Column("vct_ID", true)] // Primary Key
        public int vct_ID { get; set; }

        [Column("vct_Code")]
        public string vct_Code { get; set; }

        [Column("vct_DashboardOnlyLED")]
        public int? vct_DashboardOnlyLED { get; set; }

        [Column("vct_DomainID")]
        public int? vct_DomainID { get; set; }

        [Column("vct_MessageCode")]
        public string vct_MessageCode { get; set; }

        [Column("vct_Description")]
        public string vct_Description { get; set; }

        [Column("vct_DisplayOrder")]
        public int? vct_DisplayOrder { get; set; }

    }
}