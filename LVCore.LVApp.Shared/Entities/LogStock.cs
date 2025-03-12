namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LogStock")]
    public class LogStock
    {
        [Column("lsk_ID", true)] // Primary Key
        public int lsk_ID { get; set; }

        [Column("lsk_LogID")]
        public int? lsk_LogID { get; set; }

        [Column("lsk_LogisticUnitID")]
        public int? lsk_LogisticUnitID { get; set; }

        [Column("lsk_ProductID")]
        public int? lsk_ProductID { get; set; }

        [Column("lsk_DepositorID")]
        public int? lsk_DepositorID { get; set; }

        [Column("lsk_LocationID")]
        public int? lsk_LocationID { get; set; }

        [Column("lsk_FromContainerID")]
        public int? lsk_FromContainerID { get; set; }

        [Column("lsk_ToContainerID")]
        public int? lsk_ToContainerID { get; set; }

        [Column("lsk_FromStockID")]
        public int? lsk_FromStockID { get; set; }

        [Column("lsk_ToStockID")]
        public int? lsk_ToStockID { get; set; }

        [Column("lsk_UnsuitReasonID")]
        public int? lsk_UnsuitReasonID { get; set; }

        [Column("lsk_ReserveReasonID")]
        public int? lsk_ReserveReasonID { get; set; }

        [Column("lsk_CUQuantity")]
        public decimal? lsk_CUQuantity { get; set; }

        [Column("lsk_CUQuantityFree")]
        public decimal? lsk_CUQuantityFree { get; set; }

        [Column("lsk_ItemUnitID")]
        public int? lsk_ItemUnitID { get; set; }

        [Column("lsk_LengthQty")]
        public decimal? lsk_LengthQty { get; set; }

        [Column("lsk_LengthUnitID")]
        public int? lsk_LengthUnitID { get; set; }

        [Column("lsk_AreaQty")]
        public decimal? lsk_AreaQty { get; set; }

        [Column("lsk_AreaUnitID")]
        public int? lsk_AreaUnitID { get; set; }

        [Column("lsk_WeightQty")]
        public decimal? lsk_WeightQty { get; set; }

        [Column("lsk_WeightUnitID")]
        public int? lsk_WeightUnitID { get; set; }

        [Column("lsk_VolumeQty")]
        public decimal? lsk_VolumeQty { get; set; }

        [Column("lsk_VolumeUnitID")]
        public int? lsk_VolumeUnitID { get; set; }

        [Column("lsk_SplitStockOnCULED")]
        public int? lsk_SplitStockOnCULED { get; set; }

        [Column("lsk_DomainID")]
        public int? lsk_DomainID { get; set; }

        [Column("lsk_receiptItemID")]
        public int? lsk_receiptItemID { get; set; }

        [Column("lsk_returnItemID")]
        public int? lsk_returnItemID { get; set; }

        [Column("lsk_OriginalLED")]
        public int? lsk_OriginalLED { get; set; }

        [Column("lsk_UnitDeadWeight")]
        public decimal? lsk_UnitDeadWeight { get; set; }

    }
}