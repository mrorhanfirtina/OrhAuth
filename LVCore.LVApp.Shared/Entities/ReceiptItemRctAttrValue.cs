namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptItemRctAttrValue")]
    public class ReceiptItemRctAttrValue
    {
        [Column("rrv_ID", true)] // Primary Key
        public int rrv_ID { get; set; }

        [Column("rrv_ReceiptItemID")]
        public int? rrv_ReceiptItemID { get; set; }

        [Column("rrv_ReceiptAttributeID")]
        public int? rrv_ReceiptAttributeID { get; set; }

        [Column("rrv_Value")]
        public string rrv_Value { get; set; }

        [Column("rrv_DomainID")]
        public int? rrv_DomainID { get; set; }

    }
}