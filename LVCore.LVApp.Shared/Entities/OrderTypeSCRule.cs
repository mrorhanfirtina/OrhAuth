namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderTypeSCRule")]
    public class OrderTypeSCRule
    {
        [Column("osr_ID", true)] // Primary Key
        public int osr_ID { get; set; }

        [Column("osr_OrderTypeID")]
        public int? osr_OrderTypeID { get; set; }

        [Column("osr_SCRuleID")]
        public int? osr_SCRuleID { get; set; }

        [Column("osr_ApplyToAllCustLED")]
        public int? osr_ApplyToAllCustLED { get; set; }

        [Column("osr_ApplyToCustCatLED")]
        public int? osr_ApplyToCustCatLED { get; set; }

        [Column("osr_DomainID")]
        public int? osr_DomainID { get; set; }

    }
}