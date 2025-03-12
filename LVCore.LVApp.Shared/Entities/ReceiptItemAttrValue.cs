namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptItemAttrValue")]
    public class ReceiptItemAttrValue
    {
        [Column("riv_ID", true)] // Primary Key
        public int riv_ID { get; set; }

        [Column("riv_ReceiptItemID")]
        public int? riv_ReceiptItemID { get; set; }

        [Column("riv_AttributeID")]
        public int? riv_AttributeID { get; set; }

        [Column("riv_Value")]
        public string riv_Value { get; set; }

        [Column("riv_DomainID")]
        public int? riv_DomainID { get; set; }

    }
}