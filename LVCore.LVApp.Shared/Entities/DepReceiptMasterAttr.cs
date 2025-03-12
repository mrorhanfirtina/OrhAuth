namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepReceiptMasterAttr")]
    public class DepReceiptMasterAttr
    {
        [Column("dcm_ID", true)] // Primary Key
        public int dcm_ID { get; set; }

        [Column("dcm_DepositorID")]
        public int? dcm_DepositorID { get; set; }

        [Column("dcm_ReceiptAttributeID")]
        public int? dcm_ReceiptAttributeID { get; set; }

        [Column("dcm_DomainID")]
        public int? dcm_DomainID { get; set; }

        [Column("dcm_RequiredLED")]
        public int? dcm_RequiredLED { get; set; }

    }
}