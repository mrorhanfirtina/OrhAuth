namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepOrderOrdAttr")]
    public class DepOrderOrdAttr
    {
        [Column("doo_ID", true)] // Primary Key
        public int doo_ID { get; set; }

        [Column("doo_DepositorID")]
        public int? doo_DepositorID { get; set; }

        [Column("doo_OrderAttributeID")]
        public int? doo_OrderAttributeID { get; set; }

        [Column("doo_DomainID")]
        public int? doo_DomainID { get; set; }

        [Column("doo_RequiredLED")]
        public int? doo_RequiredLED { get; set; }

    }
}