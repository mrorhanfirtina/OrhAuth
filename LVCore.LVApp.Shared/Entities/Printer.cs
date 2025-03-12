namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Printer")]
    public class Printer
    {
        [Column("prn_ID", true)] // Primary Key
        public int prn_ID { get; set; }

        [Column("prn_Description")]
        public string prn_Description { get; set; }

        [Column("prn_PrinterTypeID")]
        public int? prn_PrinterTypeID { get; set; }

        [Column("prn_Address")]
        public string prn_Address { get; set; }

        [Column("prn_LogisticSiteID")]
        public int? prn_LogisticSiteID { get; set; }

        [Column("prn_DomainID")]
        public int? prn_DomainID { get; set; }

    }
}