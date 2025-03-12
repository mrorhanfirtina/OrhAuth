namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Depositor")]
    public class Depositor
    {
        [Column("dep_ID", true)] // Primary Key
        public int dep_ID { get; set; }

        [Column("dep_Code")]
        public string dep_Code { get; set; }

        [Column("dep_CompanyID")]
        public int? dep_CompanyID { get; set; }

        [Column("dep_ReceiptRetentionLED")]
        public int? dep_ReceiptRetentionLED { get; set; }

        [Column("dep_Memo")]
        public string dep_Memo { get; set; }

        [Column("dep_GenerateSSCCLED")]
        public int? dep_GenerateSSCCLED { get; set; }

        [Column("dep_PrintPaletLabelLED")]
        public int? dep_PrintPaletLabelLED { get; set; }

        [Column("dep_CreatePutawayTaskLED")]
        public int? dep_CreatePutawayTaskLED { get; set; }

        [Column("dep_CanModifyOrderLED")]
        public int? dep_CanModifyOrderLED { get; set; }

        [Column("dep_Validation")]
        public object dep_Validation { get; set; }

        [Column("dep_DomainID")]
        public int? dep_DomainID { get; set; }

        [Column("dep_ConfirmBackorderLED")]
        public int? dep_ConfirmBackorderLED { get; set; }

        [Column("dep_UseOrdLineInRetLineLED")]
        public int? dep_UseOrdLineInRetLineLED { get; set; }

        [Column("dep_ForbidNewRctLinesLED")]
        public int? dep_ForbidNewRctLinesLED { get; set; }

        [Column("dep_AllowDuplicateBarcodesLED")]
        public int? dep_AllowDuplicateBarcodesLED { get; set; }

        [Column("dep_MaxBarcodeLines")]
        public int? dep_MaxBarcodeLines { get; set; }

        [Column("dep_UsePatternMatchingLED")]
        public int? dep_UsePatternMatchingLED { get; set; }

        [Column("dep_ProposeRctAltUnitLED")]
        public int? dep_ProposeRctAltUnitLED { get; set; }

        [Column("dep_LoadPlanItemLine1")]
        public string dep_LoadPlanItemLine1 { get; set; }

        [Column("dep_LoadPlanItemLine2")]
        public string dep_LoadPlanItemLine2 { get; set; }

        [Column("dep_CheckSNQuantityLED")]
        public int? dep_CheckSNQuantityLED { get; set; }

        [Column("dep_CheckSNQuantityOutLED")]
        public int? dep_CheckSNQuantityOutLED { get; set; }

        [Column("dep_DefaultOrderTypeID")]
        public int? dep_DefaultOrderTypeID { get; set; }

        [Column("dep_FindPutLocOnCreationLED")]
        public int? dep_FindPutLocOnCreationLED { get; set; }

        [Column("dep_RetainMixedByContentLED")]
        public int? dep_RetainMixedByContentLED { get; set; }

        [Column("dep_ForceTaskStockLED")]
        public int? dep_ForceTaskStockLED { get; set; }

        [Column("dep_ForbidTransferResevedLED")]
        public int? dep_ForbidTransferResevedLED { get; set; }

        [Column("dep_ConfirmNewShipmentLED")]
        public int? dep_ConfirmNewShipmentLED { get; set; }

        [Column("dep_DeAllocFrom")]
        public string dep_DeAllocFrom { get; set; }

        [Column("dep_DeAllocOrderBy")]
        public string dep_DeAllocOrderBy { get; set; }

        [Column("dep_ForbidNewRetLinesLED")]
        public int? dep_ForbidNewRetLinesLED { get; set; }

        [Column("dep_AllowRsvReasonOnAllocLED")]
        public int? dep_AllowRsvReasonOnAllocLED { get; set; }

        [Column("dep_PODSignatureRequiredLED")]
        public int? dep_PODSignatureRequiredLED { get; set; }

        [Column("dep_LimitConsRefToCarrierLED")]
        public int? dep_LimitConsRefToCarrierLED { get; set; }

    }
}