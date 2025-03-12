namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ReceiptExpectLocation")]
    public class ReceiptExpectLocation
    {
        [Column("rel_ID", true)] // Primary Key
        public int rel_ID { get; set; }

        [Column("rel_receiptID")]
        public int? rel_receiptID { get; set; }

        [Column("rel_locationID")]
        public int? rel_locationID { get; set; }

        [Column("rel_Time")]
        public DateTime? rel_Time { get; set; }

        [Column("rel_Duration")]
        public int? rel_Duration { get; set; }

        [Column("rel_DomainID")]
        public int? rel_DomainID { get; set; }

    }
}