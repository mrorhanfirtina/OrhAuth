namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_SupplierDepositor")]
    public class SupplierDepositor
    {
        [Column("spd_ID", true)] // Primary Key
        public int spd_ID { get; set; }

        [Column("spd_SupplierID")]
        public int? spd_SupplierID { get; set; }

        [Column("spd_DepositorID")]
        public int? spd_DepositorID { get; set; }

        [Column("spd_DomainID")]
        public int? spd_DomainID { get; set; }

    }
}