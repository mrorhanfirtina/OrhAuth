namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptEmployee")]
    public class ReceiptEmployee
    {
        [Column("rce_ID", true)] // Primary Key
        public int rce_ID { get; set; }

        [Column("rce_ReceiptID")]
        public int? rce_ReceiptID { get; set; }

        [Column("rce_EmployeeID")]
        public int? rce_EmployeeID { get; set; }

        [Column("rce_DomainID")]
        public int? rce_DomainID { get; set; }

        [Column("rce_AssignedLED")]
        public int? rce_AssignedLED { get; set; }

        [Column("rce_ActualLED")]
        public int? rce_ActualLED { get; set; }

    }
}