namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ImpLogFile")]
    public class ImpLogFile
    {
        [Column("ilf_ID", true)] // Primary Key
        public int ilf_ID { get; set; }

        [Column("ilf_FileName")]
        public string ilf_FileName { get; set; }

        [Column("ilf_LogFileName")]
        public string ilf_LogFileName { get; set; }

        [Column("ilf_ExecDate")]
        public DateTime? ilf_ExecDate { get; set; }

        [Column("ilf_ExecFinishDate")]
        public DateTime? ilf_ExecFinishDate { get; set; }

        [Column("ilf_LastErrorMsg")]
        public string ilf_LastErrorMsg { get; set; }

        [Column("ilf_cancelledLED")]
        public int? ilf_cancelledLED { get; set; }

        [Column("ilf_ImportStatusID")]
        public int? ilf_ImportStatusID { get; set; }

        [Column("ilf_HostID")]
        public int? ilf_HostID { get; set; }

        [Column("ilf_UserID")]
        public int? ilf_UserID { get; set; }

        [Column("ilf_ImportFileID")]
        public int? ilf_ImportFileID { get; set; }

        [Column("ilf_AutolinkLED")]
        public int? ilf_AutolinkLED { get; set; }

        [Column("ilf_LogisticSiteID")]
        public int? ilf_LogisticSiteID { get; set; }

        [Column("ilf_domainID")]
        public int? ilf_domainID { get; set; }

        [Column("ilf_InstanceName")]
        public string ilf_InstanceName { get; set; }

    }
}