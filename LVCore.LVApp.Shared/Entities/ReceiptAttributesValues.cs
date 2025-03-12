namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptAttributesValues")]
    public class ReceiptAttributesValues
    {
        [Column("rav_ID", true)] // Primary Key
        public int rav_ID { get; set; }

        [Column("rav_ReceiptID")]
        public int? rav_ReceiptID { get; set; }

        [Column("rav_AttributeID")]
        public int? rav_AttributeID { get; set; }

        [Column("rav_Code")]
        public string rav_Code { get; set; }

        [Column("rav_Value")]
        public string rav_Value { get; set; }

        [Column("rav_DomainID")]
        public int? rav_DomainID { get; set; }

    }
}