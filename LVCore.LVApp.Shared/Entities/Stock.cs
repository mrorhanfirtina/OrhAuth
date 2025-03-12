namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Stock")]
    public class Stock
    {
        [Column("stk_ID", true)] // Primary Key
        public int Id { get; set; }

        [Column("stk_LocationID")]
        public int? LocationID { get; set; }

        [Column("stk_ProductID")]
        public int? ProductID { get; set; }

        [Column("stk_DepositorID")]
        public int? DepositorID { get; set; }

        [Column("stk_ContainerID")]
        public int? ContainerID { get; set; }

        [Column("stk_CUItemUnitID")]
        public int? CUItemUnitID { get; set; }

        [Column("stk_CUQuantity")]
        public decimal? CUQuantity { get; set; }

        [Column("stk_CUQuantityFree")]
        public decimal? CUQuantityFree { get; set; }

        [Column("stk_UnsuitReasonID")]
        public int? UnsuitReasonID { get; set; }

        [Column("stk_ReserveReasonID")]
        public int? ReserveReasonID { get; set; }

        [Column("stk_LockedForDAFALED")]
        public int? LockedForDAFALED { get; set; }

        [Column("stk_LengthQty")]
        public decimal? LengthQty { get; set; }

        [Column("stk_LengthUnitID")]
        public int? LengthUnitID { get; set; }

        [Column("stk_AreaQty")]
        public decimal? AreaQty { get; set; }

        [Column("stk_AreaUnitID")]
        public int? AreaUnitID { get; set; }

        [Column("stk_VolumeQty")]
        public decimal? VolumeQty { get; set; }

        [Column("stk_VolumeUnitID")]
        public int? VolumeUnitID { get; set; }

        [Column("stk_WeightQty")]
        public decimal? WeightQty { get; set; }

        [Column("stk_WeightUnitID")]
        public int? WeightUnitID { get; set; }

        [Column("stk_SplitStockInCUsLED")]
        public int? SplitStockInCUsLED { get; set; }

        [Column("stk_LocationSequence")]
        public int? LocationSequence { get; set; }

        [Column("stk_LogisticUnitID")]
        public int LogisticUnitID { get; set; } // NOT NULL

        [Column("stk_DomainID")]
        public int? DomainID { get; set; }
    }

}
