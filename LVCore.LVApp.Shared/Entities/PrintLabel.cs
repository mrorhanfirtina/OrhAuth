namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_PrintLabel")]
    public class PrintLabel
    {
        [Column("prl_ID", true)] // Primary Key
        public int prl_ID { get; set; }

        [Column("prl_Code")]
        public string prl_Code { get; set; }

        [Column("prl_Description")]
        public string prl_Description { get; set; }

        [Column("prl_LabelTypeID")]
        public int? prl_LabelTypeID { get; set; }

        [Column("prl_PrinterTypeID")]
        public int? prl_PrinterTypeID { get; set; }

        [Column("prl_LanguageID")]
        public int? prl_LanguageID { get; set; }

        [Column("prl_Content")]
        public object prl_Content { get; set; }

        [Column("prl_Parameters")]
        public object prl_Parameters { get; set; }

        [Column("prl_DepositorID")]
        public int? prl_DepositorID { get; set; }

        [Column("prl_ProductAttributeID")]
        public int? prl_ProductAttributeID { get; set; }

        [Column("prl_ProductAttributeValue")]
        public string prl_ProductAttributeValue { get; set; }

        [Column("prl_UnitID")]
        public int? prl_UnitID { get; set; }

        [Column("prl_DomainID")]
        public int? prl_DomainID { get; set; }

        [Column("prl_NumberOfCopies")]
        public int? prl_NumberOfCopies { get; set; }

        [Column("prl_LogPrintingLED")]
        public int? prl_LogPrintingLED { get; set; }

        [Column("prl_From")]
        public string prl_From { get; set; }

        [Column("prl_Fields")]
        public string prl_Fields { get; set; }

        [Column("prl_Where")]
        public string prl_Where { get; set; }

        [Column("prl_OrderBy")]
        public string prl_OrderBy { get; set; }

        [Column("prl_Encoding")]
        public string prl_Encoding { get; set; }

        [Column("prl_ReportFileExtension")]
        public string prl_ReportFileExtension { get; set; }

    }
}