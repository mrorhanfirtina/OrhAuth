namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_StockContainer")]
    public class StockContainer
    {
        [Column("stc_ID", true)] // Primary Key
        public int stc_ID { get; set; }

        [Column("stc_SSCC")]
        public string stc_SSCC { get; set; }

        [Column("stc_LocationID")]
        public int? stc_LocationID { get; set; }

        [Column("stc_ReceiptID")]
        public int? stc_ReceiptID { get; set; }

        [Column("stc_EntryDate")]
        public DateTime? stc_EntryDate { get; set; }

        [Column("stc_UnitID")]
        public int? stc_UnitID { get; set; }

        [Column("stc_GrossWeight")]
        public decimal? stc_GrossWeight { get; set; }

        [Column("stc_NetWeight")]
        public decimal? stc_NetWeight { get; set; }

        [Column("stc_WeightUnitID")]
        public int? stc_WeightUnitID { get; set; }

        [Column("stc_WeightNotDerivedLED")]
        public int? stc_WeightNotDerivedLED { get; set; }

        [Column("stc_Height")]
        public decimal? stc_Height { get; set; }

        [Column("stc_Length")]
        public decimal? stc_Length { get; set; }

        [Column("stc_Width")]
        public decimal? stc_Width { get; set; }

        [Column("stc_LengthUnitID")]
        public int? stc_LengthUnitID { get; set; }

        [Column("stc_Volume")]
        public decimal? stc_Volume { get; set; }

        [Column("stc_VolumeUnitID")]
        public int? stc_VolumeUnitID { get; set; }

        [Column("stc_RetainLED")]
        public int? stc_RetainLED { get; set; }

        [Column("stc_PrintedLabelLED")]
        public int? stc_PrintedLabelLED { get; set; }

        [Column("stc_ParentContainerID")]
        public int? stc_ParentContainerID { get; set; }

        [Column("stc_LocationSequence")]
        public int? stc_LocationSequence { get; set; }

        [Column("stc_Timestamp")]
        public DateTime? stc_Timestamp { get; set; }

        [Column("stc_DomainID")]
        public int? stc_DomainID { get; set; }

        [Column("stc_ReturnID")]
        public int? stc_ReturnID { get; set; }

        [Column("stc_Comments")]
        public string stc_Comments { get; set; }

        [Column("stc_CountedLED")]
        public int? stc_CountedLED { get; set; }

        [Column("stc_LastCountDate")]
        public DateTime? stc_LastCountDate { get; set; }

        [Column("stc_DeadWeight")]
        public decimal? stc_DeadWeight { get; set; }

        [Column("stc_WeighedLED")]
        public int? stc_WeighedLED { get; set; }

    }
}