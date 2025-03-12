namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorReceiptAttribute")]
    public class DepositorReceiptAttribute
    {
        [Column("dca_ID", true)] // Primary Key
        public int dca_ID { get; set; }

        [Column("dca_DepositorID")]
        public int? dca_DepositorID { get; set; }

        [Column("dca_StockAttributeID")]
        public int? dca_StockAttributeID { get; set; }

        [Column("dca_DomainID")]
        public int? dca_DomainID { get; set; }

        [Column("dca_RequiredLED")]
        public int? dca_RequiredLED { get; set; }

        [Column("dca_UseDefaultValueLED")]
        public int? dca_UseDefaultValueLED { get; set; }

    }
}