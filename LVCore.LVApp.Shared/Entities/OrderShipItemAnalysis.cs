namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderShipItemAnalysis")]
    public class OrderShipItemAnalysis
    {
        [Column("oia_ID", true)] // Primary Key
        public int oia_ID { get; set; }

        [Column("oia_OrderShipItemId")]
        public int? oia_OrderShipItemId { get; set; }

        [Column("oia_ItemUnitID")]
        public int? oia_ItemUnitID { get; set; }

        [Column("oia_InvAllocQty")]
        public decimal? oia_InvAllocQty { get; set; }

        [Column("oia_StockControlQty")]
        public decimal? oia_StockControlQty { get; set; }

        [Column("oia_PickListQty")]
        public decimal? oia_PickListQty { get; set; }

        [Column("oia_PickedQty")]
        public decimal? oia_PickedQty { get; set; }

        [Column("oia_SortedQty")]
        public decimal? oia_SortedQty { get; set; }

        [Column("oia_PackedQty")]
        public decimal? oia_PackedQty { get; set; }

        [Column("oia_LoadedQty")]
        public decimal? oia_LoadedQty { get; set; }

        [Column("oia_BufferQty")]
        public decimal? oia_BufferQty { get; set; }

        [Column("oia_DomainID")]
        public int? oia_DomainID { get; set; }

        [Column("oia_KitInPartsQty")]
        public decimal? oia_KitInPartsQty { get; set; }

        [Column("oia_CheckedQty")]
        public decimal? oia_CheckedQty { get; set; }

        [Column("oia_OnDemandQty")]
        public decimal? oia_OnDemandQty { get; set; }

        [Column("oia_PickedOUQty")]
        public decimal? oia_PickedOUQty { get; set; }

    }
}