namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorStockAttribute")]
    public class DepositorStockAttribute
    {
        [Column("dsa_ID", true)] // Primary Key
        public int dsa_ID { get; set; }

        [Column("dsa_DepositorID")]
        public int? dsa_DepositorID { get; set; }

        [Column("dsa_StockAttributeID")]
        public int? dsa_StockAttributeID { get; set; }

        [Column("dsa_DomainID")]
        public int? dsa_DomainID { get; set; }

    }
}