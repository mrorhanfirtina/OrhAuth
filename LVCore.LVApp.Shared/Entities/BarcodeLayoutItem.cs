namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_BarcodeLayoutItem")]
    public class BarcodeLayoutItem
    {
        [Column("bli_ID", true)] // Primary Key
        public int bli_ID { get; set; }

        [Column("bli_BarcodeLayoutID")]
        public int? bli_BarcodeLayoutID { get; set; }

        [Column("bli_FromPosition")]
        public int? bli_FromPosition { get; set; }

        [Column("bli_Size")]
        public int? bli_Size { get; set; }

        [Column("bli_BarcodeFieldID")]
        public int? bli_BarcodeFieldID { get; set; }

        [Column("bli_StockAttributeID")]
        public int? bli_StockAttributeID { get; set; }

        [Column("bli_AIID")]
        public int? bli_AIID { get; set; }

        [Column("bli_LengthUnitID")]
        public int? bli_LengthUnitID { get; set; }

        [Column("bli_WeightUnitID")]
        public int? bli_WeightUnitID { get; set; }

        [Column("bli_AreaUnitID")]
        public int? bli_AreaUnitID { get; set; }

        [Column("bli_VolumeUnitID")]
        public int? bli_VolumeUnitID { get; set; }

        [Column("bli_QtyInPiecesLED")]
        public int? bli_QtyInPiecesLED { get; set; }

        [Column("bli_YesValue")]
        public string bli_YesValue { get; set; }

        [Column("bli_NoValue")]
        public string bli_NoValue { get; set; }

        [Column("bli_Format")]
        public string bli_Format { get; set; }

        [Column("bli_DomainID")]
        public int? bli_DomainID { get; set; }

        [Column("bli_TransformSQL")]
        public string bli_TransformSQL { get; set; }

    }
}