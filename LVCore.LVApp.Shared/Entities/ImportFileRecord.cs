namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ImportFileRecord")]
    public class ImportFileRecord
    {
        [Column("ifr_ID", true)] // Primary Key
        public int ifr_ID { get; set; }

        [Column("ifr_ImpFileID")]
        public int? ifr_ImpFileID { get; set; }

        [Column("ifr_ImpRecID")]
        public int? ifr_ImpRecID { get; set; }

        [Column("ifr_ImpRecordCode")]
        public string ifr_ImpRecordCode { get; set; }

        [Column("ifr_ImpRecordTypeID")]
        public int? ifr_ImpRecordTypeID { get; set; }

        [Column("ifr_MasterRecCode")]
        public string ifr_MasterRecCode { get; set; }

        [Column("ifr_MasterCodeFieldID")]
        public int? ifr_MasterCodeFieldID { get; set; }

        [Column("ifr_DetailCodeFieldID")]
        public int? ifr_DetailCodeFieldID { get; set; }

        [Column("ifr_ExecOrder")]
        public int? ifr_ExecOrder { get; set; }

        [Column("ifr_DomainID")]
        public int? ifr_DomainID { get; set; }

        [Column("ifr_SQLOnSuccess")]
        public string ifr_SQLOnSuccess { get; set; }

        [Column("ifr_SQLOnFailure")]
        public string ifr_SQLOnFailure { get; set; }

        [Column("ifr_PreProcessSQL")]
        public string ifr_PreProcessSQL { get; set; }

    }
}