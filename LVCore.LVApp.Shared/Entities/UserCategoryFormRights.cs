namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UserCategoryFormRights")]
    public class UserCategoryFormRights
    {
        [Column("ucr_ID", true)] // Primary Key
        public int ucr_ID { get; set; }

        [Column("ucr_CategoryID")]
        public int ucr_CategoryID { get; set; }

        [Column("ucr_FormID")]
        public int? ucr_FormID { get; set; }

        [Column("ucr_AccessRight")]
        public int ucr_AccessRight { get; set; }

        [Column("ucr_DomainID")]
        public int ucr_DomainID { get; set; }

    }
}