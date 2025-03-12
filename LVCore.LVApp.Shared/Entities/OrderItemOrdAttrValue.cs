namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderItemOrdAttrValue")]
    public class OrderItemOrdAttrValue
    {
        [Column("oov_ID", true)] // Primary Key
        public int oov_ID { get; set; }

        [Column("oov_OrderItemID")]
        public int? oov_OrderItemID { get; set; }

        [Column("oov_OrderAttributeID")]
        public int? oov_OrderAttributeID { get; set; }

        [Column("oov_Value")]
        public string oov_Value { get; set; }

        [Column("oov_DomainID")]
        public int? oov_DomainID { get; set; }

    }
}