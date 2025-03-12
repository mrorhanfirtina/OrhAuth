namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_PriceListDetail")]
    public class PriceListDetail
    {
        [Column("pcr_ID", true)] // Primary Key
        public int pcr_ID { get; set; }

        [Column("pcr_FromValue")]
        public decimal? pcr_FromValue { get; set; }

        [Column("pcr_ToValue")]
        public decimal? pcr_ToValue { get; set; }

        [Column("pcr_ObjectFieldID")]
        public int? pcr_ObjectFieldID { get; set; }

        [Column("pcr_ObjectValue")]
        public string pcr_ObjectValue { get; set; }

        [Column("pcr_FixedValue")]
        public string pcr_FixedValue { get; set; }

        [Column("pcr_Price")]
        public decimal? pcr_Price { get; set; }

        [Column("pcr_PriceTypeID")]
        public int? pcr_PriceTypeID { get; set; }

        [Column("pcr_RangePriceListID")]
        public int? pcr_RangePriceListID { get; set; }

        [Column("pcr_DomainID")]
        public int? pcr_DomainID { get; set; }

        [Column("pcr_PriceListDateID")]
        public int? pcr_PriceListDateID { get; set; }

    }
}