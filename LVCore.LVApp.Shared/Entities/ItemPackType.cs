namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ItemPackType")]
    public class ItemPackType
    {
        [Column("ipt_ID", true)] // Primary Key
        public int ipt_ID { get; set; }

        [Column("ipt_ItemUnitID")]
        public int? ipt_ItemUnitID { get; set; }

        [Column("ipt_Length")]
        public decimal? ipt_Length { get; set; }

        [Column("ipt_Width")]
        public decimal? ipt_Width { get; set; }

        [Column("ipt_Height")]
        public decimal? ipt_Height { get; set; }

        [Column("ipt_LengthUnitID")]
        public int? ipt_LengthUnitID { get; set; }

        [Column("ipt_Volume")]
        public decimal? ipt_Volume { get; set; }

        [Column("ipt_VolumeUnitID")]
        public int? ipt_VolumeUnitID { get; set; }

        [Column("ipt_GeometryID")]
        public int? ipt_GeometryID { get; set; }

        [Column("ipt_DeadWeight")]
        public decimal? ipt_DeadWeight { get; set; }

        [Column("ipt_NetWeight")]
        public decimal? ipt_NetWeight { get; set; }

        [Column("ipt_WeightUnitID")]
        public int? ipt_WeightUnitID { get; set; }

        [Column("ipt_EmptyDimensionsLED")]
        public int? ipt_EmptyDimensionsLED { get; set; }

        [Column("ipt_ContainerLED")]
        public int? ipt_ContainerLED { get; set; }

        [Column("ipt_SSCCCode")]
        public int? ipt_SSCCCode { get; set; }

        [Column("ipt_StowageFactor")]
        public int? ipt_StowageFactor { get; set; }

        [Column("ipt_DomainID")]
        public int? ipt_DomainID { get; set; }

        [Column("ipt_WidthAsHeightLED")]
        public int? ipt_WidthAsHeightLED { get; set; }

        [Column("ipt_LengthAsHeightLED")]
        public int? ipt_LengthAsHeightLED { get; set; }

        [Column("ipt_NestedHeight")]
        public decimal? ipt_NestedHeight { get; set; }

        [Column("ipt_NestedLength")]
        public decimal? ipt_NestedLength { get; set; }

        [Column("ipt_NestedWidth")]
        public decimal? ipt_NestedWidth { get; set; }

    }
}