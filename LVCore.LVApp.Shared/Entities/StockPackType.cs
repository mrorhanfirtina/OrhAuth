namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_StockPackType")]
    public class StockPackType
    {
        [Column("spt_ID", true)] // Primary Key
        public int spt_ID { get; set; }

        [Column("spt_StockID")]
        public int? spt_StockID { get; set; }

        [Column("spt_ItemUnitID")]
        public int? spt_ItemUnitID { get; set; }

        [Column("spt_Quantity")]
        public decimal? spt_Quantity { get; set; }

        [Column("spt_QuantityFree")]
        public decimal? spt_QuantityFree { get; set; }

        [Column("spt_PackTypeRatio")]
        public decimal? spt_PackTypeRatio { get; set; }

        [Column("spt_ParentID")]
        public int? spt_ParentID { get; set; }

        [Column("spt_CUQuantity")]
        public decimal? spt_CUQuantity { get; set; }

        [Column("spt_Timestamp")]
        public DateTime? spt_Timestamp { get; set; }

        [Column("spt_DomainID")]
        public int? spt_DomainID { get; set; }

        [Column("spt_CUQuantityFree")]
        public decimal? spt_CUQuantityFree { get; set; }

    }
}