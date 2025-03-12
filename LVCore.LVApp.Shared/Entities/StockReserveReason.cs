namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_StockReserveReason")]
    public class StockReserveReason
    {
        [Column("srr_ID", true)] // Primary Key
        public int srr_ID { get; set; }

        [Column("srr_Code")]
        public string srr_Code { get; set; }

        [Column("srr_Description")]
        public string srr_Description { get; set; }

        [Column("srr_ReserveInvLED")]
        public int? srr_ReserveInvLED { get; set; }

        [Column("srr_DomainID")]
        public int? srr_DomainID { get; set; }

        [Column("srr_ApplyToAllocatedLED")]
        public int? srr_ApplyToAllocatedLED { get; set; }

    }
}