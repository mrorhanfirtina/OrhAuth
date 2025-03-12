namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_StandardPackType")]
    public class StandardPackType
    {
        [Column("sdp_ID", true)] // Primary Key
        public int sdp_ID { get; set; }

        [Column("sdp_UnitID")]
        public int? sdp_UnitID { get; set; }

        [Column("sdp_BasePackTypeID")]
        public int? sdp_BasePackTypeID { get; set; }

        [Column("sdp_Length")]
        public decimal? sdp_Length { get; set; }

        [Column("sdp_Width")]
        public decimal? sdp_Width { get; set; }

        [Column("sdp_Height")]
        public decimal? sdp_Height { get; set; }

        [Column("sdp_LengthUnitID")]
        public int? sdp_LengthUnitID { get; set; }

        [Column("sdp_Volume")]
        public decimal? sdp_Volume { get; set; }

        [Column("sdp_VolumeUnitID")]
        public int? sdp_VolumeUnitID { get; set; }

        [Column("sdp_GeometryID")]
        public int? sdp_GeometryID { get; set; }

        [Column("sdp_DeadWeight")]
        public decimal? sdp_DeadWeight { get; set; }

        [Column("sdp_WeightUnitID")]
        public int? sdp_WeightUnitID { get; set; }

        [Column("sdp_EmptyDimensionsLED")]
        public int? sdp_EmptyDimensionsLED { get; set; }

        [Column("sdp_ContainerLED")]
        public int? sdp_ContainerLED { get; set; }

        [Column("sdp_MixedLED")]
        public int? sdp_MixedLED { get; set; }

        [Column("sdp_SSCCCode")]
        public int? sdp_SSCCCode { get; set; }

        [Column("sdp_DomainID")]
        public int? sdp_DomainID { get; set; }

        [Column("sdp_MultipleBillingPacksLED")]
        public int? sdp_MultipleBillingPacksLED { get; set; }

        [Column("sdp_PackingPackTypeLED")]
        public int? sdp_PackingPackTypeLED { get; set; }

        [Column("sdp_PackingHeight")]
        public decimal? sdp_PackingHeight { get; set; }

        [Column("sdp_PackingHeightMargin")]
        public decimal? sdp_PackingHeightMargin { get; set; }

        [Column("sdp_PackingVolume")]
        public decimal? sdp_PackingVolume { get; set; }

        [Column("sdp_MaxWeight")]
        public decimal? sdp_MaxWeight { get; set; }

        [Column("sdp_TransportContainerLED")]
        public int? sdp_TransportContainerLED { get; set; }

        [Column("sdp_BondItemID")]
        public int? sdp_BondItemID { get; set; }

        [Column("sdp_WeightDeviation")]
        public decimal? sdp_WeightDeviation { get; set; }

        [Column("sdp_WDevPercentLED")]
        public int? sdp_WDevPercentLED { get; set; }

        [Column("sdp_RetainEmptyLED")]
        public int? sdp_RetainEmptyLED { get; set; }

        [Column("sdp_SingleContentLED")]
        public int? sdp_SingleContentLED { get; set; }

        [Column("sdp_SingleItemLED")]
        public int? sdp_SingleItemLED { get; set; }

        [Column("sdp_KeepEmptyBondInLocLED")]
        public int? sdp_KeepEmptyBondInLocLED { get; set; }

        [Column("sdp_SingleAttrPerItem")]
        public string sdp_SingleAttrPerItem { get; set; }

    }
}