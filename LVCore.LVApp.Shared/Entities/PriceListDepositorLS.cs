namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_PriceListDepositorLS")]
    public class PriceListDepositorLS
    {
        [Column("pdl_ID", true)] // Primary Key
        public int pdl_ID { get; set; }

        [Column("pdl_PriceListID")]
        public int? pdl_PriceListID { get; set; }

        [Column("pdl_DepositorID")]
        public int? pdl_DepositorID { get; set; }

        [Column("pdl_LogisticSiteID")]
        public int? pdl_LogisticSiteID { get; set; }

        [Column("pdl_LogisticUnitID")]
        public int? pdl_LogisticUnitID { get; set; }

        [Column("pdl_DomainID")]
        public int? pdl_DomainID { get; set; }

    }
}