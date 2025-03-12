namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UserListFormat")]
    public class UserListFormat
    {
        [Column("ulf_ID", true)] // Primary Key
        public int ulf_ID { get; set; }

        [Column("ulf_UserID")]
        public int? ulf_UserID { get; set; }

        [Column("ulf_FormID")]
        public int? ulf_FormID { get; set; }

        [Column("ulf_FormCode")]
        public string ulf_FormCode { get; set; }

        [Column("ulf_Format")]
        public string ulf_Format { get; set; }

        [Column("ulf_DomainID")]
        public int? ulf_DomainID { get; set; }

        [Column("ulf_StatusBar")]
        public string ulf_StatusBar { get; set; }

        [Column("ulf_From")]
        public string ulf_From { get; set; }

        [Column("ulf_Where")]
        public string ulf_Where { get; set; }

        [Column("ulf_Fields")]
        public string ulf_Fields { get; set; }

        [Column("ulf_PageSize")]
        public int? ulf_PageSize { get; set; }

    }
}