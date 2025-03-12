namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ShipStockAttrValues")]
    public class ShipStockAttrValues
    {
        [Column("ssv_ID", true)] // Primary Key
        public int ssv_ID { get; set; }

        [Column("ssv_ShipStockID")]
        public int? ssv_ShipStockID { get; set; }

        [Column("ssv_attributeID")]
        public int? ssv_attributeID { get; set; }

        [Column("ssv_Value")]
        public string ssv_Value { get; set; }

        [Column("ssv_DomainID")]
        public int? ssv_DomainID { get; set; }

    }
}