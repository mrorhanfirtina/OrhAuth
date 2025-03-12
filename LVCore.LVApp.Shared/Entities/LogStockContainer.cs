namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_LogStockContainer")]
    public class LogStockContainer
    {
        [Column("Lsc_ID", true)] // Primary Key
        public int Lsc_ID { get; set; }

        [Column("lsc_LogID")]
        public int? lsc_LogID { get; set; }

        [Column("Lsc_SSCC")]
        public string Lsc_SSCC { get; set; }

        [Column("Lsc_LocationID")]
        public int? Lsc_LocationID { get; set; }

        [Column("Lsc_ReceiptID")]
        public int? Lsc_ReceiptID { get; set; }

        [Column("Lsc_EntryDate")]
        public DateTime? Lsc_EntryDate { get; set; }

        [Column("lsc_UnitID")]
        public int? lsc_UnitID { get; set; }

        [Column("Lsc_GrossWeight")]
        public decimal? Lsc_GrossWeight { get; set; }

        [Column("Lsc_NetWeight")]
        public decimal? Lsc_NetWeight { get; set; }

        [Column("lsc_WeightUnitID")]
        public int? lsc_WeightUnitID { get; set; }

        [Column("Lsc_Height")]
        public decimal? Lsc_Height { get; set; }

        [Column("Lsc_Length")]
        public decimal? Lsc_Length { get; set; }

        [Column("Lsc_Width")]
        public decimal? Lsc_Width { get; set; }

        [Column("Lsc_UOMDimensionID")]
        public int? Lsc_UOMDimensionID { get; set; }

        [Column("lsc_LengthUnitID")]
        public int? lsc_LengthUnitID { get; set; }

        [Column("Lsc_Volume")]
        public decimal? Lsc_Volume { get; set; }

        [Column("lsc_VolumeUnitID")]
        public int? lsc_VolumeUnitID { get; set; }

        [Column("lsc_WeightNotDerivedLED")]
        public int? lsc_WeightNotDerivedLED { get; set; }

        [Column("lsc_PrintedLabelLED")]
        public int? lsc_PrintedLabelLED { get; set; }

        [Column("lsc_LocationSequence")]
        public int? lsc_LocationSequence { get; set; }

        [Column("Lsc_RetainLED")]
        public int? Lsc_RetainLED { get; set; }

        [Column("Lsc_ParentContainerID")]
        public int? Lsc_ParentContainerID { get; set; }

        [Column("Lsc_DomainID")]
        public int? Lsc_DomainID { get; set; }

        [Column("lsc_Comments")]
        public string lsc_Comments { get; set; }

        [Column("lsc_DeadWeight")]
        public decimal? lsc_DeadWeight { get; set; }

        [Column("lsc_ReturnID")]
        public int? lsc_ReturnID { get; set; }

    }
}