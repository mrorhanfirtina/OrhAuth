namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_TaskStock")]
    public class TaskStock
    {
        [Column("tks_ID", true)] // Primary Key
        public int tks_ID { get; set; }

        [Column("tks_TaskID")]
        public int? tks_TaskID { get; set; }

        [Column("tks_StockID")]
        public int? tks_StockID { get; set; }

        [Column("tks_PackTypeID")]
        public int? tks_PackTypeID { get; set; }

        [Column("tks_Quantity")]
        public decimal? tks_Quantity { get; set; }

        [Column("tks_Timestamp")]
        public DateTime? tks_Timestamp { get; set; }

        [Column("tks_DomainID")]
        public int? tks_DomainID { get; set; }

    }
}