namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ReceiptLocation")]
    public class ReceiptLocation
    {
        [Column("rcl_ID", true)] // Primary Key
        public int rcl_ID { get; set; }

        [Column("rcl_ReceiptID")]
        public int? rcl_ReceiptID { get; set; }

        [Column("rcl_LocationID")]
        public int? rcl_LocationID { get; set; }

        [Column("rcl_Time")]
        public DateTime? rcl_Time { get; set; }

        [Column("rcl_Duration")]
        public decimal? rcl_Duration { get; set; }

        [Column("rcl_DomainID")]
        public int? rcl_DomainID { get; set; }

    }
}