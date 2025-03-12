namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LogStockPackType")]
    public class LogStockPackType
    {
        [Column("lsp_ID", true)] // Primary Key
        public int lsp_ID { get; set; }

        [Column("lsp_LogStockID")]
        public int? lsp_LogStockID { get; set; }

        [Column("lsp_ItemUnitID")]
        public int? lsp_ItemUnitID { get; set; }

        [Column("lsp_Quantity")]
        public decimal? lsp_Quantity { get; set; }

        [Column("lsp_QuantityFree")]
        public decimal? lsp_QuantityFree { get; set; }

        [Column("lsp_PackTypeRatio")]
        public decimal? lsp_PackTypeRatio { get; set; }

        [Column("lsp_ParentID")]
        public int? lsp_ParentID { get; set; }

        [Column("lsp_DomainID")]
        public int? lsp_DomainID { get; set; }

        [Column("lsp_receiptItemID")]
        public int? lsp_receiptItemID { get; set; }

        [Column("lsp_returnItemID")]
        public int? lsp_returnItemID { get; set; }

        [Column("lsp_CUQuantity")]
        public decimal? lsp_CUQuantity { get; set; }

    }
}