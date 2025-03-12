namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ImportFile")]
    public class ImportFile
    {
        [Column("imp_ID", true)] // Primary Key
        public int imp_ID { get; set; }

        [Column("imp_Description")]
        public string imp_Description { get; set; }

        [Column("imp_DepositorID")]
        public int? imp_DepositorID { get; set; }

        [Column("imp_InputFileTypeID")]
        public int? imp_InputFileTypeID { get; set; }

        [Column("imp_IgnoreFirst")]
        public int? imp_IgnoreFirst { get; set; }

        [Column("imp_IgnoreLast")]
        public int? imp_IgnoreLast { get; set; }

        [Column("imp_minLinLen")]
        public int? imp_minLinLen { get; set; }

        [Column("imp_ConvTypeID")]
        public int? imp_ConvTypeID { get; set; }

        [Column("imp_ConvFile")]
        public string imp_ConvFile { get; set; }

        [Column("imp_LineDelimiter")]
        public string imp_LineDelimiter { get; set; }

        [Column("imp_ColDelimiter")]
        public string imp_ColDelimiter { get; set; }

        [Column("imp_RecordCodeCol")]
        public int? imp_RecordCodeCol { get; set; }

        [Column("imp_RecordCodeSize")]
        public int? imp_RecordCodeSize { get; set; }

        [Column("imp_RecordCodePosition")]
        public int? imp_RecordCodePosition { get; set; }

        [Column("imp_XSLFile")]
        public string imp_XSLFile { get; set; }

        [Column("imp_InputDir")]
        public string imp_InputDir { get; set; }

        [Column("imp_BackupDir")]
        public string imp_BackupDir { get; set; }

        [Column("imp_LogDir")]
        public string imp_LogDir { get; set; }

        [Column("imp_SendMailOnErrorLED")]
        public int? imp_SendMailOnErrorLED { get; set; }

        [Column("imp_SendMailOnOKLED")]
        public int? imp_SendMailOnOKLED { get; set; }

        [Column("imp_AutoImportLED")]
        public int? imp_AutoImportLED { get; set; }

        [Column("imp_FileMask")]
        public string imp_FileMask { get; set; }

        [Column("imp_SeparateDetailFileLED")]
        public int? imp_SeparateDetailFileLED { get; set; }

        [Column("imp_DtlIgnoreFirst")]
        public int? imp_DtlIgnoreFirst { get; set; }

        [Column("imp_DtlIgnoreLast")]
        public int? imp_DtlIgnoreLast { get; set; }

        [Column("imp_DtlMinLinLen")]
        public int? imp_DtlMinLinLen { get; set; }

        [Column("imp_DtlInputDir")]
        public string imp_DtlInputDir { get; set; }

        [Column("imp_DtlFileMask")]
        public string imp_DtlFileMask { get; set; }

        [Column("imp_ImportNotifyEmail")]
        public string imp_ImportNotifyEmail { get; set; }

        [Column("imp_AutoImpExecOrder")]
        public int? imp_AutoImpExecOrder { get; set; }

        [Column("imp_ReplyType")]
        public int? imp_ReplyType { get; set; }

        [Column("imp_ReplyReceiptLED")]
        public int? imp_ReplyReceiptLED { get; set; }

        [Column("imp_ReplySuccessLED")]
        public int? imp_ReplySuccessLED { get; set; }

        [Column("imp_ReplyFailureLED")]
        public int? imp_ReplyFailureLED { get; set; }

        [Column("imp_ReceiptReplyTemplate")]
        public string imp_ReceiptReplyTemplate { get; set; }

        [Column("imp_ReceiptReplyFileMask")]
        public string imp_ReceiptReplyFileMask { get; set; }

        [Column("imp_ReceiptReplyDir")]
        public string imp_ReceiptReplyDir { get; set; }

        [Column("imp_ProcessReplyTemplate")]
        public string imp_ProcessReplyTemplate { get; set; }

        [Column("imp_ProcessReplyFileMask")]
        public string imp_ProcessReplyFileMask { get; set; }

        [Column("imp_ProcessReplyDir")]
        public string imp_ProcessReplyDir { get; set; }

        [Column("imp_ThousandSymbol")]
        public string imp_ThousandSymbol { get; set; }

        [Column("imp_DecimalSymbol")]
        public string imp_DecimalSymbol { get; set; }

        [Column("imp_BackUpFileMask")]
        public string imp_BackUpFileMask { get; set; }

        [Column("imp_LogFileMask")]
        public string imp_LogFileMask { get; set; }

        [Column("imp_DomainID")]
        public int? imp_DomainID { get; set; }

        [Column("imp_FilterTag")]
        public string imp_FilterTag { get; set; }

        [Column("imp_FilterValues")]
        public string imp_FilterValues { get; set; }

        [Column("imp_RejectedDir")]
        public string imp_RejectedDir { get; set; }

        [Column("imp_CopyDir")]
        public string imp_CopyDir { get; set; }

        [Column("imp_KeepLogTableLED")]
        public int? imp_KeepLogTableLED { get; set; }

        [Column("imp_QuotedStringsLED")]
        public int? imp_QuotedStringsLED { get; set; }

        [Column("imp_PreprocessAssemblyName")]
        public string imp_PreprocessAssemblyName { get; set; }

        [Column("imp_PreprocessTypeName")]
        public string imp_PreprocessTypeName { get; set; }

        [Column("imp_AllowMissingFieldsLED")]
        public int? imp_AllowMissingFieldsLED { get; set; }

        [Column("imp_AutoLinkInstance")]
        public string imp_AutoLinkInstance { get; set; }

        [Column("imp_ReplyAssemblyName")]
        public string imp_ReplyAssemblyName { get; set; }

        [Column("imp_ReplyTypeName")]
        public string imp_ReplyTypeName { get; set; }

        [Column("imp_FolderStructure")]
        public string imp_FolderStructure { get; set; }

        [Column("imp_SendMailOnWarningLED")]
        public int? imp_SendMailOnWarningLED { get; set; }

    }
}