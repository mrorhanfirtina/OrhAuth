namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderItemAttrValue")]
    public class OrderItemAttrValue
    {
        [Column("oiv_ID", true)] // Primary Key
        public int oiv_ID { get; set; }

        [Column("oiv_OrderItemID")]
        public int? oiv_OrderItemID { get; set; }

        [Column("oiv_AttributeID")]
        public int? oiv_AttributeID { get; set; }

        [Column("oiv_Value")]
        public string oiv_Value { get; set; }

        [Column("oiv_DomainID")]
        public int? oiv_DomainID { get; set; }

    }
}