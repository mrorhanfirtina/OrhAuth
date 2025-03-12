namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_ScheduledJobDetail")]
    public class ScheduledJobDetail
    {
        [Column("sjd_ID", true)] // Primary Key
        public int sjd_ID { get; set; }

        [Column("sjd_ScheduledJobID")]
        public int? sjd_ScheduledJobID { get; set; }

        [Column("sjd_SQLCommand")]
        public object sjd_SQLCommand { get; set; }

        [Column("sjd_DomainID")]
        public int? sjd_DomainID { get; set; }

    }
}