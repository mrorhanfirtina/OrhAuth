namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Session")]
    public class Session
    {
        [Column("ses_ID", true)] // Primary Key
        public int ses_ID { get; set; }

        [Column("ses_Code")]
        public string ses_Code { get; set; }

        [Column("ses_UserID")]
        public int? ses_UserID { get; set; }

        [Column("ses_HostID")]
        public int? ses_HostID { get; set; }

        [Column("ses_StartTime")]
        public DateTime? ses_StartTime { get; set; }

        [Column("ses_EndTime")]
        public DateTime? ses_EndTime { get; set; }

        [Column("ses_DomainID")]
        public int? ses_DomainID { get; set; }

        [Column("ses_ClarkID")]
        public int? ses_ClarkID { get; set; }

    }
}