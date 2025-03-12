namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Location")]
    public class Location
    {
        [Column("loc_ID", true)] // Primary Key
        public int loc_ID { get; set; }

        [Column("loc_Code")]
        public string loc_Code { get; set; }

        [Column("loc_StorageSystemID")]
        public int? loc_StorageSystemID { get; set; }

        [Column("loc_BuildingID")]
        public int? loc_BuildingID { get; set; }

        [Column("loc_SectorCode")]
        public string loc_SectorCode { get; set; }

        [Column("loc_Sector")]
        public int? loc_Sector { get; set; }

        [Column("loc_ColumnCode")]
        public string loc_ColumnCode { get; set; }

        [Column("loc_Column")]
        public int? loc_Column { get; set; }

        [Column("loc_Side")]
        public int? loc_Side { get; set; }

        [Column("loc_LevelCode")]
        public string loc_LevelCode { get; set; }

        [Column("loc_Level")]
        public int? loc_Level { get; set; }

        [Column("loc_SubLevelCode")]
        public string loc_SubLevelCode { get; set; }

        [Column("loc_SubColumnCode")]
        public string loc_SubColumnCode { get; set; }

        [Column("loc_SubPositionCode")]
        public string loc_SubPositionCode { get; set; }

        [Column("loc_Width")]
        public decimal? loc_Width { get; set; }

        [Column("loc_Height")]
        public decimal? loc_Height { get; set; }

        [Column("loc_Length")]
        public decimal? loc_Length { get; set; }

        [Column("loc_LengthUnitID")]
        public int? loc_LengthUnitID { get; set; }

        [Column("loc_Volume")]
        public decimal? loc_Volume { get; set; }

        [Column("loc_VolumeUnitID")]
        public int? loc_VolumeUnitID { get; set; }

        [Column("loc_MaxWeight")]
        public decimal? loc_MaxWeight { get; set; }

        [Column("loc_WeightUnitID")]
        public int? loc_WeightUnitID { get; set; }

        [Column("loc_BalanceLED")]
        public int? loc_BalanceLED { get; set; }

        [Column("loc_PickingTypeID")]
        public int? loc_PickingTypeID { get; set; }

        [Column("loc_ERPStoreID")]
        public int? loc_ERPStoreID { get; set; }

        [Column("loc_CapacityStatusID")]
        public int? loc_CapacityStatusID { get; set; }

        [Column("loc_ExpCapacityStatusID")]
        public int? loc_ExpCapacityStatusID { get; set; }

        [Column("loc_LockLED")]
        public int? loc_LockLED { get; set; }

        [Column("loc_LockReasonID")]
        public int? loc_LockReasonID { get; set; }

        [Column("loc_DepositorID")]
        public int? loc_DepositorID { get; set; }

        [Column("loc_StockControlLED")]
        public int? loc_StockControlLED { get; set; }

        [Column("loc_ReceivingLED")]
        public int? loc_ReceivingLED { get; set; }

        [Column("loc_SendingLED")]
        public int? loc_SendingLED { get; set; }

        [Column("loc_DamagedLED")]
        public int? loc_DamagedLED { get; set; }

        [Column("loc_ReturnsLED")]
        public int? loc_ReturnsLED { get; set; }

        [Column("loc_PackingLED")]
        public int? loc_PackingLED { get; set; }

        [Column("loc_SortingLED")]
        public int? loc_SortingLED { get; set; }

        [Column("loc_PickingLED")]
        public int? loc_PickingLED { get; set; }

        [Column("loc_RepackingLED")]
        public int? loc_RepackingLED { get; set; }

        [Column("loc_VANReturnsLED")]
        public int? loc_VANReturnsLED { get; set; }

        [Column("loc_ReplenishSourceLED")]
        public int? loc_ReplenishSourceLED { get; set; }

        [Column("loc_ReplenishTargetLED")]
        public int? loc_ReplenishTargetLED { get; set; }

        [Column("loc_Counter")]
        public int? loc_Counter { get; set; }

        [Column("loc_InboundPriority")]
        public int? loc_InboundPriority { get; set; }

        [Column("loc_OutboundPriority")]
        public int? loc_OutboundPriority { get; set; }

        [Column("loc_LogisticUnitID")]
        public int loc_LogisticUnitID { get; set; }

        [Column("loc_NoStockMergeLED")]
        public int? loc_NoStockMergeLED { get; set; }

        [Column("loc_DynamicConstraintLED")]
        public int? loc_DynamicConstraintLED { get; set; }

        [Column("Loc_CombinationMeasureID")]
        public int? Loc_CombinationMeasureID { get; set; }

        [Column("loc_DomainID")]
        public int? loc_DomainID { get; set; }

        [Column("loc_CountDate")]
        public DateTime? loc_CountDate { get; set; }

        [Column("loc_Description")]
        public string loc_Description { get; set; }

        [Column("loc_CheckDigits")]
        public string loc_CheckDigits { get; set; }

        [Column("loc_ReplWholeContainerLED")]
        public int? loc_ReplWholeContainerLED { get; set; }

        [Column("loc_TransitLED")]
        public int? loc_TransitLED { get; set; }

        [Column("loc_LevelMaxWeight")]
        public decimal? loc_LevelMaxWeight { get; set; }

        [Column("loc_ColumnMaxWeight")]
        public decimal? loc_ColumnMaxWeight { get; set; }

        [Column("loc_SingleProductLED")]
        public int? loc_SingleProductLED { get; set; }

        [Column("loc_SinglePackTypeLED")]
        public int? loc_SinglePackTypeLED { get; set; }

        [Column("loc_TypicalLevel")]
        public int? loc_TypicalLevel { get; set; }

        [Column("loc_UnsuitabilityReasonID")]
        public int? loc_UnsuitabilityReasonID { get; set; }

        [Column("loc_ReserveReasonID")]
        public int? loc_ReserveReasonID { get; set; }

        [Column("loc_SingleDepositorLED")]
        public int? loc_SingleDepositorLED { get; set; }

        [Column("Loc_CapacityAttributeID")]
        public int? Loc_CapacityAttributeID { get; set; }

        [Column("loc_RejectReserveReasonLED")]
        public int? loc_RejectReserveReasonLED { get; set; }

        [Column("loc_RejectUnsuitReasonLED")]
        public int? loc_RejectUnsuitReasonLED { get; set; }

    }
}