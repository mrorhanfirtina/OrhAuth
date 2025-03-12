namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ImpLogRecord")]
    public class ImpLogRecord
    {
        [Column("ilr_ID", true)] // Primary Key
        public int ilr_ID { get; set; }

        [Column("ilr_ImpLogFileID")]
        public int ilr_ImpLogFileID { get; set; }

        [Column("ilr_ImportRecordID")]
        public int? ilr_ImportRecordID { get; set; }

        [Column("ilr_SuccessLED")]
        public int? ilr_SuccessLED { get; set; }

        [Column("ilr_FinishDate")]
        public DateTime? ilr_FinishDate { get; set; }

        [Column("ilr_InsertedRecords")]
        public int? ilr_InsertedRecords { get; set; }

        [Column("ilr_failedRecords")]
        public int? ilr_failedRecords { get; set; }

        [Column("ilr_UpdatedRecords")]
        public int? ilr_UpdatedRecords { get; set; }

        [Column("ilr_LogMessage")]
        public string ilr_LogMessage { get; set; }

        [Column("ilr_DomainID")]
        public int? ilr_DomainID { get; set; }

    }
}