namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ShipStock")]
    public class ShipStock
    {
        [Column("shs_ID", true)] // Primary Key
        public int shs_ID { get; set; }

        [Column("shs_ShipmentID")]
        public int? shs_ShipmentID { get; set; }

        [Column("shs_ProductID")]
        public int? shs_ProductID { get; set; }

        [Column("shs_DepositorID")]
        public int? shs_DepositorID { get; set; }

        [Column("shs_ContainerID")]
        public int? shs_ContainerID { get; set; }

        [Column("shs_CUItemUnitID")]
        public int? shs_CUItemUnitID { get; set; }

        [Column("shs_CUQuantity")]
        public decimal? shs_CUQuantity { get; set; }

        [Column("shs_UnsuitReasonID")]
        public int? shs_UnsuitReasonID { get; set; }

        [Column("shs_LengthQty")]
        public decimal? shs_LengthQty { get; set; }

        [Column("shs_LengthUnitID")]
        public int? shs_LengthUnitID { get; set; }

        [Column("shs_AreaQty")]
        public decimal? shs_AreaQty { get; set; }

        [Column("shs_AreaUnitID")]
        public int? shs_AreaUnitID { get; set; }

        [Column("shs_VolumeQty")]
        public decimal? shs_VolumeQty { get; set; }

        [Column("shs_VolumeUnitID")]
        public int? shs_VolumeUnitID { get; set; }

        [Column("shs_WeightQty")]
        public decimal? shs_WeightQty { get; set; }

        [Column("shs_WeightUnitID")]
        public int? shs_WeightUnitID { get; set; }

        [Column("shs_SplitStockInCUsLED")]
        public int? shs_SplitStockInCUsLED { get; set; }

        [Column("shs_DomainID")]
        public int? shs_DomainID { get; set; }

        [Column("shs_UnitDeadWeight")]
        public decimal? shs_UnitDeadWeight { get; set; }

    }
}