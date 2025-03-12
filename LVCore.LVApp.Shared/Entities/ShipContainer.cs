namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ShipContainer")]
    public class ShipContainer
    {
        [Column("shc_ID", true)] // Primary Key
        public int shc_ID { get; set; }

        [Column("shc_SSCC")]
        public string shc_SSCC { get; set; }

        [Column("shc_ContainerID")]
        public int? shc_ContainerID { get; set; }

        [Column("shc_Sequence")]
        public int? shc_Sequence { get; set; }

        [Column("shc_OrderShipmentID")]
        public int? shc_OrderShipmentID { get; set; }

        [Column("shc_OrderShipGroupID")]
        public int? shc_OrderShipGroupID { get; set; }

        [Column("shc_ShipmentID")]
        public int? shc_ShipmentID { get; set; }

        [Column("shc_BillingPackTotal")]
        public int? shc_BillingPackTotal { get; set; }

        [Column("shc_UnitID")]
        public int? shc_UnitID { get; set; }

        [Column("shc_GrossWeight")]
        public decimal? shc_GrossWeight { get; set; }

        [Column("shc_NetWeight")]
        public decimal? shc_NetWeight { get; set; }

        [Column("shc_WeightUnitID")]
        public int? shc_WeightUnitID { get; set; }

        [Column("shc_Height")]
        public decimal? shc_Height { get; set; }

        [Column("shc_Length")]
        public decimal? shc_Length { get; set; }

        [Column("shc_Width")]
        public decimal? shc_Width { get; set; }

        [Column("shc_LengthUnitID")]
        public int? shc_LengthUnitID { get; set; }

        [Column("shc_Volume")]
        public decimal? shc_Volume { get; set; }

        [Column("shc_VolumeUnitID")]
        public int? shc_VolumeUnitID { get; set; }

        [Column("shc_DomainID")]
        public int? shc_DomainID { get; set; }

        [Column("shc_CreateDate")]
        public DateTime? shc_CreateDate { get; set; }

        [Column("shc_WeightNotDerivedLED")]
        public int? shc_WeightNotDerivedLED { get; set; }

        [Column("shc_LoadDate")]
        public DateTime? shc_LoadDate { get; set; }

        [Column("shc_CreatedByLoadingLED")]
        public int? shc_CreatedByLoadingLED { get; set; }

        [Column("shc_ParentContainerID")]
        public int? shc_ParentContainerID { get; set; }

        [Column("shc_ParentLED")]
        public int? shc_ParentLED { get; set; }

        [Column("shc_Comments")]
        public string shc_Comments { get; set; }

        [Column("shc_EntryDate")]
        public DateTime? shc_EntryDate { get; set; }

        [Column("shc_PrintedLabelLED")]
        public int? shc_PrintedLabelLED { get; set; }

        [Column("shc_ExportedLED")]
        public int? shc_ExportedLED { get; set; }

        [Column("shc_CheckedLED")]
        public int? shc_CheckedLED { get; set; }

        [Column("shc_DeadWeight")]
        public decimal? shc_DeadWeight { get; set; }

        [Column("shc_WeighedLED")]
        public int? shc_WeighedLED { get; set; }

    }
}