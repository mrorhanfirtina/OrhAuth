namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepOrderMasterAttr")]
    public class DepOrderMasterAttr
    {
        [Column("dom_ID", true)] // Primary Key
        public int dom_ID { get; set; }

        [Column("dom_DepositorID")]
        public int? dom_DepositorID { get; set; }

        [Column("dom_OrderAttributeID")]
        public int? dom_OrderAttributeID { get; set; }

        [Column("dom_DomainID")]
        public int? dom_DomainID { get; set; }

        [Column("dom_RequiredLED")]
        public int? dom_RequiredLED { get; set; }

    }
}