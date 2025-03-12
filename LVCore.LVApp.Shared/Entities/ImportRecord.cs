namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ImportRecord")]
    public class ImportRecord
    {
        [Column("imr_ID", true)] // Primary Key
        public int imr_ID { get; set; }

        [Column("imr_Description")]
        public string imr_Description { get; set; }

        [Column("imr_TargetSchemaID")]
        public int? imr_TargetSchemaID { get; set; }

        [Column("imr_InputFileTypeID")]
        public int? imr_InputFileTypeID { get; set; }

        [Column("imr_RecordRows")]
        public int? imr_RecordRows { get; set; }

        [Column("imr_XMLRecordTag")]
        public string imr_XMLRecordTag { get; set; }

        [Column("imr_FilterInFieldID")]
        public int? imr_FilterInFieldID { get; set; }

        [Column("imr_FilterInValue")]
        public string imr_FilterInValue { get; set; }

        [Column("imr_FilterOutFieldID")]
        public int? imr_FilterOutFieldID { get; set; }

        [Column("imr_FilterOutValue")]
        public string imr_FilterOutValue { get; set; }

        [Column("imr_SQLStatement")]
        public string imr_SQLStatement { get; set; }

        [Column("imr_Identifier")]
        public string imr_Identifier { get; set; }

        [Column("imr_Timestamp")]
        public DateTime? imr_Timestamp { get; set; }

        [Column("imr_DomainID")]
        public int? imr_DomainID { get; set; }

        [Column("imr_SubRecCodeCol")]
        public int? imr_SubRecCodeCol { get; set; }

        [Column("imr_SubRecCodeSize")]
        public int? imr_SubRecCodeSize { get; set; }

        [Column("imr_SubRecCodePos")]
        public int? imr_SubRecCodePos { get; set; }

        [Column("imr_MasterSubRecCode")]
        public string imr_MasterSubRecCode { get; set; }

        [Column("imr_MergeDetailLED")]
        public int? imr_MergeDetailLED { get; set; }

        [Column("imr_InsertOnlyLED")]
        public int? imr_InsertOnlyLED { get; set; }

        [Column("imr_SourceSelectSQL")]
        public string imr_SourceSelectSQL { get; set; }

        [Column("imr_SourceUpdateSQL")]
        public string imr_SourceUpdateSQL { get; set; }

        [Column("imr_SourceConnectionString")]
        public string imr_SourceConnectionString { get; set; }

        [Column("imr_LogRecordsLED")]
        public int? imr_LogRecordsLED { get; set; }

        [Column("imr_LogSkippedLED")]
        public int? imr_LogSkippedLED { get; set; }

        [Column("imr_LogFailedLED")]
        public int? imr_LogFailedLED { get; set; }

        [Column("imr_LogSuccessLED")]
        public int? imr_LogSuccessLED { get; set; }

        [Column("imr_IDTag1")]
        public string imr_IDTag1 { get; set; }

        [Column("imr_IDTag2")]
        public string imr_IDTag2 { get; set; }

        [Column("imr_CaseSensitiveLED")]
        public int? imr_CaseSensitiveLED { get; set; }

    }
}