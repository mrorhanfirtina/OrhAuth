namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UserRoles")]
    public class UserRoles
    {
        [Column("uro_ID", true)] // Primary Key
        public int uro_ID { get; set; }

        [Column("uro_UserID")]
        public int uro_UserID { get; set; }

        [Column("uro_CategoryID")]
        public int uro_CategoryID { get; set; }

        [Column("uro_DomainID")]
        public int uro_DomainID { get; set; }

    }
}