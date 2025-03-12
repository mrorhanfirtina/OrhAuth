namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderType")]
    public class OrderType
    {
        [Column("ort_ID", true)] // Primary Key
        public int ort_ID { get; set; }

        [Column("ort_Code")]
        public string ort_Code { get; set; }

        [Column("ort_Description")]
        public string ort_Description { get; set; }

        [Column("ort_ScenarioID")]
        public int? ort_ScenarioID { get; set; }

        [Column("ort_UnlockSUPackTypeLED")]
        public int? ort_UnlockSUPackTypeLED { get; set; }

        [Column("ort_ReturnToSupplierLED")]
        public int? ort_ReturnToSupplierLED { get; set; }

        [Column("ort_SupplyProductionLED")]
        public int? ort_SupplyProductionLED { get; set; }

        [Column("ort_UseQuotasLED")]
        public int? ort_UseQuotasLED { get; set; }

        [Column("ort_AutoAllocateLED")]
        public int? ort_AutoAllocateLED { get; set; }

        [Column("ort_CreateBackOrderLED")]
        public int? ort_CreateBackOrderLED { get; set; }

        [Column("ort_UseAttrPickPolicyLED")]
        public int? ort_UseAttrPickPolicyLED { get; set; }

        [Column("ort_SplitOrderShipLED")]
        public int? ort_SplitOrderShipLED { get; set; }

        [Column("ort_CreateLoadTasksLED")]
        public int? ort_CreateLoadTasksLED { get; set; }

        [Column("ort_NoStockActionID")]
        public int? ort_NoStockActionID { get; set; }

        [Column("ort_PickingLocationID")]
        public int? ort_PickingLocationID { get; set; }

        [Column("ort_RemovePickedStockLED")]
        public int? ort_RemovePickedStockLED { get; set; }

        [Column("ort_DomainID")]
        public int? ort_DomainID { get; set; }

        [Column("ort_ManualSortingLED")]
        public int? ort_ManualSortingLED { get; set; }

        [Column("ort_NoMergeLinesInPickTaskLED")]
        public int? ort_NoMergeLinesInPickTaskLED { get; set; }

        [Column("ort_AcceptInactiveItemsLED")]
        public int? ort_AcceptInactiveItemsLED { get; set; }

        [Column("ort_AlgorithmID")]
        public int? ort_AlgorithmID { get; set; }

        [Column("ort_ForbidStockSwapLED")]
        public int? ort_ForbidStockSwapLED { get; set; }

        [Column("ort_ItemCancelOnPickDelLED")]
        public int? ort_ItemCancelOnPickDelLED { get; set; }

        [Column("ort_RoundingID")]
        public int? ort_RoundingID { get; set; }

        [Column("ort_KeepLoadedStockLED")]
        public int? ort_KeepLoadedStockLED { get; set; }

        [Column("ort_FreePartialPickRestLed")]
        public int? ort_FreePartialPickRestLed { get; set; }

        [Column("ort_KeepShortagesPendingLED")]
        public int? ort_KeepShortagesPendingLED { get; set; }

        [Column("ort_KeepRoutingOnDeallocLED")]
        public int? ort_KeepRoutingOnDeallocLED { get; set; }

        [Column("ort_TryPickSingleLULED")]
        public int? ort_TryPickSingleLULED { get; set; }

        [Column("ort_PickFromAnyLULED")]
        public int? ort_PickFromAnyLULED { get; set; }

        [Column("ort_CreateProdOrderLED")]
        public int? ort_CreateProdOrderLED { get; set; }

        [Column("ort_ReleaseProdOrderLED")]
        public int? ort_ReleaseProdOrderLED { get; set; }

        [Column("ort_CheckPickingLED")]
        public int? ort_CheckPickingLED { get; set; }

        [Column("ort_ProposePickTargetLocLED")]
        public int? ort_ProposePickTargetLocLED { get; set; }

        [Column("ort_DepositorTransferLED")]
        public int? ort_DepositorTransferLED { get; set; }

        [Column("ort_PackWhilePickingLED")]
        public int? ort_PackWhilePickingLED { get; set; }

        [Column("ort_ReplAlgorithmID")]
        public int? ort_ReplAlgorithmID { get; set; }

        [Column("ort_PackLED")]
        public int? ort_PackLED { get; set; }

        [Column("ort_WarnOnPolicyBreachLED")]
        public int? ort_WarnOnPolicyBreachLED { get; set; }

        [Column("ort_InsertItemsInPickingLED")]
        public int? ort_InsertItemsInPickingLED { get; set; }

        [Column("ort_CancelNoStockOrderLED")]
        public int? ort_CancelNoStockOrderLED { get; set; }

        [Column("ort_MixedOrderPacksLED")]
        public int? ort_MixedOrderPacksLED { get; set; }

        [Column("ort_SpecialStockControlID")]
        public int? ort_SpecialStockControlID { get; set; }

        [Column("ort_BackOrderTypeID")]
        public int? ort_BackOrderTypeID { get; set; }

        [Column("ort_PackMultipleLocationsLED")]
        public int? ort_PackMultipleLocationsLED { get; set; }

        [Column("ort_ForbidPolicyBreachLED")]
        public int? ort_ForbidPolicyBreachLED { get; set; }

        [Column("ort_CustomListRptName")]
        public string ort_CustomListRptName { get; set; }

        [Column("ort_FinalAssemblyName")]
        public string ort_FinalAssemblyName { get; set; }

        [Column("ort_FinalTypeName")]
        public string ort_FinalTypeName { get; set; }

        [Column("ort_AcceptUnsuitableStockLED")]
        public int? ort_AcceptUnsuitableStockLED { get; set; }

        [Column("ort_AcceptReservedStockLED")]
        public int? ort_AcceptReservedStockLED { get; set; }

        [Column("ort_PrePackingAlgorithmID")]
        public int? ort_PrePackingAlgorithmID { get; set; }

        [Column("ort_ShortageSpreadMethod")]
        public int? ort_ShortageSpreadMethod { get; set; }

        [Column("ort_CreateOffTimeReplLED")]
        public int? ort_CreateOffTimeReplLED { get; set; }

        [Column("ort_ItemCancelOnLoadDel")]
        public int? ort_ItemCancelOnLoadDel { get; set; }

        [Column("ort_ProcessMethod")]
        public int? ort_ProcessMethod { get; set; }

        [Column("ort_PutawayScenarioID")]
        public int? ort_PutawayScenarioID { get; set; }

        [Column("ort_AttributeChangeLED")]
        public int? ort_AttributeChangeLED { get; set; }

        [Column("ort_AttributeID")]
        public int? ort_AttributeID { get; set; }

        [Column("ort_AttributeValue")]
        public string ort_AttributeValue { get; set; }

        [Column("ort_AllowPartialHardAllocLED")]
        public int? ort_AllowPartialHardAllocLED { get; set; }

        [Column("ort_CheckPackLED")]
        public int? ort_CheckPackLED { get; set; }

        [Column("ort_DestLocListWhere")]
        public string ort_DestLocListWhere { get; set; }

        [Column("ort_FreePickedStockLED")]
        public int? ort_FreePickedStockLED { get; set; }

        [Column("ort_WarehouseReplLED")]
        public int? ort_WarehouseReplLED { get; set; }

        [Column("ort_BonusStrategyID")]
        public int? ort_BonusStrategyID { get; set; }

        [Column("ort_LoadWhilePickingLED")]
        public int? ort_LoadWhilePickingLED { get; set; }

        [Column("ort_CreateSingleListLED")]
        public int? ort_CreateSingleListLED { get; set; }

        [Column("ort_CreateManifestLED")]
        public int? ort_CreateManifestLED { get; set; }

        [Column("ort_ManifestationStep")]
        public int? ort_ManifestationStep { get; set; }

        [Column("ort_ManifestPerRecipientLED")]
        public int? ort_ManifestPerRecipientLED { get; set; }

        [Column("ort_DefConsTypeID")]
        public int? ort_DefConsTypeID { get; set; }

        [Column("ort_PickAttributeChangeLED")]
        public int? ort_PickAttributeChangeLED { get; set; }

        [Column("ort_ReserveUnsuitChangeLED")]
        public int? ort_ReserveUnsuitChangeLED { get; set; }

        [Column("ort_ReserveUnsuitReasonValue")]
        public string ort_ReserveUnsuitReasonValue { get; set; }

        [Column("ort_PickResUnsuitChangeLED")]
        public int? ort_PickResUnsuitChangeLED { get; set; }

        [Column("ort_ReplenishOnlyLED")]
        public int? ort_ReplenishOnlyLED { get; set; }

        [Column("ort_PickDepositorTransferLED")]
        public int? ort_PickDepositorTransferLED { get; set; }

        [Column("ort_AcceptQCStockLED")]
        public int? ort_AcceptQCStockLED { get; set; }

        [Column("ort_GroupingExclusionSQL")]
        public string ort_GroupingExclusionSQL { get; set; }

    }
}