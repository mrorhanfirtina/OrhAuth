namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ShipStockPackType")]
    public class ShipStockPackType
    {
        [Column("ssp_ID", true)] // Primary Key
        public int ssp_ID { get; set; }

        [Column("ssp_ShipStockID")]
        public int? ssp_ShipStockID { get; set; }

        [Column("ssp_ItemUnitID")]
        public int? ssp_ItemUnitID { get; set; }

        [Column("ssp_Quantity")]
        public decimal? ssp_Quantity { get; set; }

        [Column("ssp_PackTypeRatio")]
        public decimal? ssp_PackTypeRatio { get; set; }

        [Column("ssp_ParentID")]
        public int? ssp_ParentID { get; set; }

        [Column("ssp_CUQuantity")]
        public decimal? ssp_CUQuantity { get; set; }

        [Column("ssp_DomainID")]
        public int? ssp_DomainID { get; set; }

    }
}