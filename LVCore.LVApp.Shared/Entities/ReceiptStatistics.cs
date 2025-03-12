namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptStatistics")]
    public class ReceiptStatistics
    {
        [Column("rst_ID", true)] // Primary Key
        public int rst_ID { get; set; }

        [Column("rst_ProductID")]
        public int? rst_ProductID { get; set; }

        [Column("rst_InputItemUnitID")]
        public int? rst_InputItemUnitID { get; set; }

        [Column("rst_ReceiptItemUnitID")]
        public int? rst_ReceiptItemUnitID { get; set; }

        [Column("rst_Clarks")]
        public int? rst_Clarks { get; set; }

        [Column("rst_Hands")]
        public int? rst_Hands { get; set; }

        [Column("rst_Duration")]
        public decimal? rst_Duration { get; set; }

        [Column("rst_SampleSize")]
        public int? rst_SampleSize { get; set; }

        [Column("rst_LogisticSiteID")]
        public int? rst_LogisticSiteID { get; set; }

        [Column("rst_DomainID")]
        public int? rst_DomainID { get; set; }

    }
}