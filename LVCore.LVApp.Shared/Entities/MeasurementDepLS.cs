namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_MeasurementDepLS")]
    public class MeasurementDepLS
    {
        [Column("mdl_ID", true)] // Primary Key
        public int mdl_ID { get; set; }

        [Column("mdl_MeasurementID")]
        public int? mdl_MeasurementID { get; set; }

        [Column("mdl_DepositorID")]
        public int? mdl_DepositorID { get; set; }

        [Column("mdl_LogisticSiteID")]
        public int? mdl_LogisticSiteID { get; set; }

        [Column("mdl_LogisticUnitID")]
        public int? mdl_LogisticUnitID { get; set; }

        [Column("mdl_THENPriceListID")]
        public int? mdl_THENPriceListID { get; set; }

        [Column("mdl_ELSEPriceListID")]
        public int? mdl_ELSEPriceListID { get; set; }

        [Column("mdl_FNameForPrice")]
        public string mdl_FNameForPrice { get; set; }

        [Column("mdl_DomainID")]
        public int? mdl_DomainID { get; set; }

        [Column("mdl_THENPrice")]
        public decimal? mdl_THENPrice { get; set; }

        [Column("mdl_ELSEPrice")]
        public decimal? mdl_ELSEPrice { get; set; }

    }
}