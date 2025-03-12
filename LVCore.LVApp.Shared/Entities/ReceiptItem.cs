namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ReceiptItem")]
    public class ReceiptItem
    {
        [Column("rci_ID", true)] // Primary Key
        public int rci_ID { get; set; }

        [Column("rci_ReceiptID")]
        public int? rci_ReceiptID { get; set; }

        [Column("rci_ProductID")]
        public int? rci_ProductID { get; set; }

        [Column("rci_ProdItemID")]
        public int? rci_ProdItemID { get; set; }

        [Column("rci_ExpQuantity")]
        public decimal? rci_ExpQuantity { get; set; }

        [Column("rci_TaskQuantity")]
        public decimal? rci_TaskQuantity { get; set; }

        [Column("rci_ActItemUnitID")]
        public int? rci_ActItemUnitID { get; set; }

        [Column("rci_ActQuantity")]
        public decimal? rci_ActQuantity { get; set; }

        [Column("rci_InputItemUnitID")]
        public int? rci_InputItemUnitID { get; set; }

        [Column("rci_RetentionPercentage")]
        public decimal? rci_RetentionPercentage { get; set; }

        [Column("rci_RetainedQuantity")]
        public decimal? rci_RetainedQuantity { get; set; }

        [Column("rci_ReceivedContQty")]
        public decimal? rci_ReceivedContQty { get; set; }

        [Column("rci_ExpectLocationID")]
        public int? rci_ExpectLocationID { get; set; }

        [Column("rci_LocationID")]
        public int? rci_LocationID { get; set; }

        [Column("rci_GenerateSSCCLED")]
        public int? rci_GenerateSSCCLED { get; set; }

        [Column("rci_POrderItemID")]
        public int? rci_POrderItemID { get; set; }

        [Column("rci_ActualDate")]
        public DateTime? rci_ActualDate { get; set; }

        [Column("rci_ExpVolume")]
        public decimal? rci_ExpVolume { get; set; }

        [Column("rci_ExpGrossWeight")]
        public decimal? rci_ExpGrossWeight { get; set; }

        [Column("rci_ExpNetWeight")]
        public decimal? rci_ExpNetWeight { get; set; }

        [Column("rci_VolumeUnitID")]
        public int? rci_VolumeUnitID { get; set; }

        [Column("rci_WeightUnitID")]
        public int? rci_WeightUnitID { get; set; }

        [Column("rci_LogisticUnitID")]
        public int? rci_LogisticUnitID { get; set; }

        [Column("rci_DomainID")]
        public int? rci_DomainID { get; set; }

        [Column("rci_ReceiptLine")]
        public int? rci_ReceiptLine { get; set; }

        [Column("rci_SSCC")]
        public string rci_SSCC { get; set; }

        [Column("rci_DocumentQty")]
        public decimal? rci_DocumentQty { get; set; }

        [Column("rci_DocItemUnitID")]
        public int? rci_DocItemUnitID { get; set; }

        [Column("rci_ExplodedQty")]
        public decimal? rci_ExplodedQty { get; set; }

        [Column("rci_ImplodedQty")]
        public decimal? rci_ImplodedQty { get; set; }

        [Column("rci_NonKitQty")]
        public decimal? rci_NonKitQty { get; set; }

        [Column("rci_ReserveReasonID")]
        public int? rci_ReserveReasonID { get; set; }

        [Column("rci_Price")]
        public decimal? rci_Price { get; set; }

    }
}