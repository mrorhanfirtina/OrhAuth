namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Product")]
    public class Product
    {
        [Column("prd_ID", true)] // Primary Key
        public int prd_ID { get; set; }

        [Column("prd_PrimaryCode")]
        public string prd_PrimaryCode { get; set; }

        [Column("prd_SecondaryCode")]
        public string prd_SecondaryCode { get; set; }

        [Column("prd_TemplateID")]
        public int? prd_TemplateID { get; set; }

        [Column("prd_InactiveLED")]
        public int? prd_InactiveLED { get; set; }

        [Column("prd_TemplateLED")]
        public int? prd_TemplateLED { get; set; }

        [Column("prd_BondLED")]
        public int? prd_BondLED { get; set; }

        [Column("prd_ItemTypeID")]
        public int? prd_ItemTypeID { get; set; }

        [Column("prd_PhantomLED")]
        public int? prd_PhantomLED { get; set; }

        [Column("prd_VariantSeq")]
        public int? prd_VariantSeq { get; set; }

        [Column("prd_VarCodeGenerateLED")]
        public int? prd_VarCodeGenerateLED { get; set; }

        [Column("prd_LotControlledLED")]
        public int? prd_LotControlledLED { get; set; }

        [Column("prd_VariantControlledLED")]
        public int? prd_VariantControlledLED { get; set; }

        [Column("prd_UseExtInvLED")]
        public int? prd_UseExtInvLED { get; set; }

        [Column("prd_AllocateQCStockLED")]
        public int? prd_AllocateQCStockLED { get; set; }

        [Column("prd_Validation")]
        public string prd_Validation { get; set; }

        [Column("prd_UseLocalValidationLED")]
        public int? prd_UseLocalValidationLED { get; set; }

        [Column("prd_NoInventoryLED")]
        public int? prd_NoInventoryLED { get; set; }

        [Column("prd_DomainID")]
        public int? prd_DomainID { get; set; }

        [Column("prd_LotSeq")]
        public int? prd_LotSeq { get; set; }

        [Column("prd_LotFormula")]
        public string prd_LotFormula { get; set; }

        [Column("prd_LotLastSeqRefVal")]
        public string prd_LotLastSeqRefVal { get; set; }

        [Column("prd_KeepPickTasksInSULED")]
        public int? prd_KeepPickTasksInSULED { get; set; }

        [Column("prd_UseWholeUnitLED")]
        public int? prd_UseWholeUnitLED { get; set; }

        [Column("prd_ReceiptReserveReasonID")]
        public int? prd_ReceiptReserveReasonID { get; set; }

        [Column("prd_PackingMaterialLED")]
        public int? prd_PackingMaterialLED { get; set; }

        [Column("prd_PutByFIFOLED")]
        public int? prd_PutByFIFOLED { get; set; }

        [Column("prd_KeepKitInvLED")]
        public int? prd_KeepKitInvLED { get; set; }

        [Column("prd_TryPutInPickingSectorLED")]
        public int? prd_TryPutInPickingSectorLED { get; set; }

        [Column("prd_MergeDifAltQtyLED")]
        public int? prd_MergeDifAltQtyLED { get; set; }

        [Column("prd_QtyPerOrderLabel")]
        public decimal? prd_QtyPerOrderLabel { get; set; }

        [Column("prd_AllowFreeStockAttrLED")]
        public int? prd_AllowFreeStockAttrLED { get; set; }

        [Column("prd_NoAccountStockLED")]
        public int? prd_NoAccountStockLED { get; set; }

        [Column("prd_LastUpdateTime")]
        public DateTime? prd_LastUpdateTime { get; set; }

        [Column("prd_AlcoholLED")]
        public int? prd_AlcoholLED { get; set; }

        [Column("prd_HZMTypeID")]
        public int? prd_HZMTypeID { get; set; }

        [Column("prd_BatteryInContentTypeID")]
        public int? prd_BatteryInContentTypeID { get; set; }

        [Column("prd_BatteryMaterialTypeID")]
        public int? prd_BatteryMaterialTypeID { get; set; }

        [Column("prd_OriginCountryID")]
        public int? prd_OriginCountryID { get; set; }

        [Column("prd_ProducerCompanyID")]
        public int? prd_ProducerCompanyID { get; set; }

        [Column("prd_NAFTAProducerTypeID")]
        public int? prd_NAFTAProducerTypeID { get; set; }

        [Column("prd_NAFTANetCostMethodID")]
        public int? prd_NAFTANetCostMethodID { get; set; }

        [Column("prd_NAFTAPreferenceCriterionID")]
        public int? prd_NAFTAPreferenceCriterionID { get; set; }

        [Column("prd_NAFTANetCostFromDate")]
        public DateTime? prd_NAFTANetCostFromDate { get; set; }

        [Column("prd_NAFTANetCostToDate")]
        public DateTime? prd_NAFTANetCostToDate { get; set; }

    }
}