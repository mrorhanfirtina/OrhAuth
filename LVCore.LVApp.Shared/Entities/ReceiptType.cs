namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptType")]
    public class ReceiptType
    {
        [Column("rtt_ID", true)] // Primary Key
        public int rtt_ID { get; set; }

        [Column("rtt_Code")]
        public string rtt_Code { get; set; }

        [Column("rtt_Description")]
        public string rtt_Description { get; set; }

        [Column("rtt_DomainID")]
        public int? rtt_DomainID { get; set; }

    }
}