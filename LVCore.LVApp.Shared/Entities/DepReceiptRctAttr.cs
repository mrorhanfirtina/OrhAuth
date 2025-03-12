namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepReceiptRctAttr")]
    public class DepReceiptRctAttr
    {
        [Column("drr_ID", true)] // Primary Key
        public int drr_ID { get; set; }

        [Column("drr_DepositorID")]
        public int? drr_DepositorID { get; set; }

        [Column("drr_ReceiptAttributeID")]
        public int? drr_ReceiptAttributeID { get; set; }

        [Column("drr_DomainID")]
        public int? drr_DomainID { get; set; }

        [Column("drr_RequiredLED")]
        public int? drr_RequiredLED { get; set; }

    }
}