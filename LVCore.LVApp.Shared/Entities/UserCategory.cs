namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UserCategory")]
    public class UserCategory
    {
        [Column("usc_ID", true)] // Primary Key
        public int usc_ID { get; set; }

        [Column("usc_Code")]
        public string usc_Code { get; set; }

        [Column("usc_Description")]
        public string usc_Description { get; set; }

        [Column("usc_Memo")]
        public string usc_Memo { get; set; }

        [Column("usc_DomainID")]
        public int usc_DomainID { get; set; }

        [Column("usc_UsePackItemButtonLED")]
        public int? usc_UsePackItemButtonLED { get; set; }

        [Column("usc_AllDashboardsLED")]
        public int? usc_AllDashboardsLED { get; set; }

    }
}