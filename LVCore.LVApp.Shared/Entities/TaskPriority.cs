namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TaskPriority")]
    public class TaskPriority
    {
        [Column("tpy_ID", true)] // Primary Key
        public int tpy_ID { get; set; }

        [Column("tpy_Order")]
        public int? tpy_Order { get; set; }

        [Column("tpy_DomainID")]
        public int? tpy_DomainID { get; set; }

    }
}