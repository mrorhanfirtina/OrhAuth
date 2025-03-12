namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("COM_LSParameters")]
    public class LSParameters
    {
        [Column("lpm_ID", true)] // Primary Key
        public int lpm_ID { get; set; }

        [Column("lpm_LogisticSiteID")]
        public int? lpm_LogisticSiteID { get; set; }

        [Column("lpm_TaskLED")]
        public int lpm_TaskLED { get; set; }

        [Column("lpm_ReceiptRetentionLED")]
        public int? lpm_ReceiptRetentionLED { get; set; }

        [Column("lpm_POCodeGenerateLED")]
        public int? lpm_POCodeGenerateLED { get; set; }

        [Column("lpm_SplitStockItemsLED")]
        public int? lpm_SplitStockItemsLED { get; set; }

        [Column("lpm_ReceiptCodeGenerateLED")]
        public int? lpm_ReceiptCodeGenerateLED { get; set; }

        [Column("lpm_ReturnCodeGenerateLED")]
        public int? lpm_ReturnCodeGenerateLED { get; set; }

        [Column("lpm_OrderCodeGenerateLED")]
        public int? lpm_OrderCodeGenerateLED { get; set; }

        [Column("lpm_PuttingOverrideLED")]
        public int? lpm_PuttingOverrideLED { get; set; }

        [Column("lpm_LocCodeFormat")]
        public string lpm_LocCodeFormat { get; set; }

        [Column("lpm_SSCCCountryCode")]
        public string lpm_SSCCCountryCode { get; set; }

        [Column("lpm_SSCCCompanyCode")]
        public string lpm_SSCCCompanyCode { get; set; }

        [Column("lpm_SSCCCounter")]
        public int? lpm_SSCCCounter { get; set; }

        [Column("lpm_DAFALocationID")]
        public int? lpm_DAFALocationID { get; set; }

        [Column("lpm_TransitLocationID")]
        public int? lpm_TransitLocationID { get; set; }

        [Column("lpm_BondLocationID")]
        public int? lpm_BondLocationID { get; set; }

        [Column("lpm_ChangeStockWithTasksLED")]
        public int? lpm_ChangeStockWithTasksLED { get; set; }

        [Column("lpm_WarnForStockTasksLED")]
        public int? lpm_WarnForStockTasksLED { get; set; }

        [Column("lpm_ReplFromOtherBldLED")]
        public int? lpm_ReplFromOtherBldLED { get; set; }

        [Column("lpm_ReplReservedStockLED")]
        public int? lpm_ReplReservedStockLED { get; set; }

        [Column("lpm_DomainID")]
        public int? lpm_DomainID { get; set; }

        [Column("lpm_DelFreeReplTaskLED")]
        public int? lpm_DelFreeReplTaskLED { get; set; }

        [Column("lpm_ReplThreshold")]
        public int? lpm_ReplThreshold { get; set; }

        [Column("lpm_ForbidCapacityExcessLED")]
        public int? lpm_ForbidCapacityExcessLED { get; set; }

        [Column("lpm_ShipmentCodeGenerateLED")]
        public int? lpm_ShipmentCodeGenerateLED { get; set; }

        [Column("lpm_IgnoreCapacityOnReplLED")]
        public int? lpm_IgnoreCapacityOnReplLED { get; set; }

        [Column("lpm_EnterPackDimensionsLED")]
        public int? lpm_EnterPackDimensionsLED { get; set; }

        [Column("lpm_PartialLoadString")]
        public string lpm_PartialLoadString { get; set; }

        [Column("lpm_KeepPalletOnReplLED")]
        public int? lpm_KeepPalletOnReplLED { get; set; }

        [Column("lpm_BreakBlockedPackTypeLED")]
        public int? lpm_BreakBlockedPackTypeLED { get; set; }

        [Column("lpm_CheckSNQuantityLED")]
        public int? lpm_CheckSNQuantityLED { get; set; }

        [Column("lpm_PutawayScenarioID")]
        public int? lpm_PutawayScenarioID { get; set; }

        [Column("lpm_ListNoAgencyOrdersByGSLED")]
        public int? lpm_ListNoAgencyOrdersByGSLED { get; set; }

        [Column("lpm_CheckSNQuantityOutLED")]
        public int? lpm_CheckSNQuantityOutLED { get; set; }

        [Column("lpm_CheckUnroutedOrdersLED")]
        public int? lpm_CheckUnroutedOrdersLED { get; set; }

        [Column("lpm_CalculatePackDimensionsLED")]
        public int? lpm_CalculatePackDimensionsLED { get; set; }

        [Column("lpm_UseBufferLocationsLED")]
        public int? lpm_UseBufferLocationsLED { get; set; }

        [Column("lpm_CheckTopPTInMustReplLED")]
        public int? lpm_CheckTopPTInMustReplLED { get; set; }

        [Column("lpm_ForceTaskStockLED")]
        public int? lpm_ForceTaskStockLED { get; set; }

        [Column("lpm_MapServerName")]
        public string lpm_MapServerName { get; set; }

        [Column("lpm_MapServerPort")]
        public int? lpm_MapServerPort { get; set; }

        [Column("lpm_MapAssembly")]
        public string lpm_MapAssembly { get; set; }

        [Column("lpm_MapTypeName")]
        public string lpm_MapTypeName { get; set; }

        [Column("lpm_PickCountPlanCodeFormat")]
        public string lpm_PickCountPlanCodeFormat { get; set; }

        [Column("lpm_PickCountPlanDescription")]
        public string lpm_PickCountPlanDescription { get; set; }

        [Column("lpm_CheckForkliftUsageLED")]
        public int? lpm_CheckForkliftUsageLED { get; set; }

        [Column("lpm_DefaultDeliveryDuration")]
        public int? lpm_DefaultDeliveryDuration { get; set; }

        [Column("lpm_LockedLocStockUpdateLED")]
        public int? lpm_LockedLocStockUpdateLED { get; set; }

        [Column("lpm_UpdateCorrectedAddressLED")]
        public int? lpm_UpdateCorrectedAddressLED { get; set; }

        [Column("lpm_ManualReceiptCompletionLED")]
        public int? lpm_ManualReceiptCompletionLED { get; set; }

        [Column("lpm_ManualReturnCompletionLED")]
        public int? lpm_ManualReturnCompletionLED { get; set; }

        [Column("lpm_ExcessReserveReasonID")]
        public int? lpm_ExcessReserveReasonID { get; set; }

        [Column("lpm_UnlinkedReserveReasonID")]
        public int? lpm_UnlinkedReserveReasonID { get; set; }

        [Column("lpm_NoAutoERDetailLinkLED")]
        public int? lpm_NoAutoERDetailLinkLED { get; set; }

        [Column("lpm_NoUseOtherSectorTruckLED")]
        public int? lpm_NoUseOtherSectorTruckLED { get; set; }

        [Column("lpm_NoMixedSectorShipmentsLED")]
        public int? lpm_NoMixedSectorShipmentsLED { get; set; }

        [Column("lpm_AddBondInReceiptLED")]
        public int? lpm_AddBondInReceiptLED { get; set; }

        [Column("lpm_CheckCoexistInPalletsLED")]
        public int? lpm_CheckCoexistInPalletsLED { get; set; }

        [Column("lpm_StockLocPackFromStockLED")]
        public int? lpm_StockLocPackFromStockLED { get; set; }

        [Column("lpm_ConsignCodeGenerateLED")]
        public int? lpm_ConsignCodeGenerateLED { get; set; }

        [Column("lpm_ManifestCodeGenerateLED")]
        public int? lpm_ManifestCodeGenerateLED { get; set; }

        [Column("lpm_HSProductAttributeID")]
        public int? lpm_HSProductAttributeID { get; set; }

        [Column("lpm_HTSProductAttributeID")]
        public int? lpm_HTSProductAttributeID { get; set; }

        [Column("lpm_CountryStockAttributeID")]
        public int? lpm_CountryStockAttributeID { get; set; }

        [Column("lpm_ETDUploadLED")]
        public int? lpm_ETDUploadLED { get; set; }

        [Column("lpm_ETDDocUploadStepID")]
        public int? lpm_ETDDocUploadStepID { get; set; }

        [Column("lpm_ETDPath")]
        public string lpm_ETDPath { get; set; }

        [Column("lpm_SNPerSSCCLED")]
        public int? lpm_SNPerSSCCLED { get; set; }

        [Column("lpm_NMFAClassProdAttrID")]
        public int? lpm_NMFAClassProdAttrID { get; set; }

        [Column("lpm_NMFACodeProdAttrID")]
        public int? lpm_NMFACodeProdAttrID { get; set; }

        [Column("lpm_PickupTimeFrom")]
        public DateTime? lpm_PickupTimeFrom { get; set; }

        [Column("lpm_PickupTimeTo")]
        public DateTime? lpm_PickupTimeTo { get; set; }

    }
}