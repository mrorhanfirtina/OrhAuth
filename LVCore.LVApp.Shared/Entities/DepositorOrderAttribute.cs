namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorOrderAttribute")]
    public class DepositorOrderAttribute
    {
        [Column("doa_ID", true)] // Primary Key
        public int doa_ID { get; set; }

        [Column("doa_DepositorID")]
        public int? doa_DepositorID { get; set; }

        [Column("doa_StockAttributeID")]
        public int? doa_StockAttributeID { get; set; }

        [Column("doa_DomainID")]
        public int? doa_DomainID { get; set; }

        [Column("doa_RequiredLED")]
        public int? doa_RequiredLED { get; set; }

        [Column("doa_UseDefaultValueLED")]
        public int? doa_UseDefaultValueLED { get; set; }

    }
}