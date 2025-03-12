namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LocPutSuggestion")]
    public class LocPutSuggestion
    {
        [Column("lps_ID", true)] // Primary Key
        public int lps_ID { get; set; }

        [Column("lps_LocationID")]
        public int? lps_LocationID { get; set; }

        [Column("lps_StockID")]
        public int? lps_StockID { get; set; }

        [Column("lps_ContainerID")]
        public int? lps_ContainerID { get; set; }

        [Column("lps_StockPackTypeID")]
        public int? lps_StockPackTypeID { get; set; }

        [Column("lps_Quantity")]
        public decimal? lps_Quantity { get; set; }

        [Column("lps_Weight")]
        public decimal? lps_Weight { get; set; }

        [Column("lps_WeightUnitID")]
        public int? lps_WeightUnitID { get; set; }

        [Column("lps_Volume")]
        public decimal? lps_Volume { get; set; }

        [Column("lps_VolumeUnitID")]
        public int? lps_VolumeUnitID { get; set; }

        [Column("lps_TaskID")]
        public int? lps_TaskID { get; set; }

        [Column("lps_DomainID")]
        public int? lps_DomainID { get; set; }

    }
}