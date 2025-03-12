namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UserDepositor")]
    public class UserDepositor
    {
        [Column("usd_ID", true)] // Primary Key
        public int usd_ID { get; set; }

        [Column("usd_UserID")]
        public int? usd_UserID { get; set; }

        [Column("usd_DepositorID")]
        public int? usd_DepositorID { get; set; }

        [Column("usd_DomainID")]
        public int? usd_DomainID { get; set; }

    }
}