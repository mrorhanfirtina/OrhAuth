namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LocPrinter")]
    public class LocPrinter
    {
        [Column("lpr_ID", true)] // Primary Key
        public int lpr_ID { get; set; }

        [Column("lpr_LocationID")]
        public int? lpr_LocationID { get; set; }

        [Column("lpr_LabelTypeID")]
        public int? lpr_LabelTypeID { get; set; }

        [Column("lpr_Printer")]
        public string lpr_Printer { get; set; }

        [Column("lpr_DomainID")]
        public int? lpr_DomainID { get; set; }

        [Column("lpr_PrintLabelID")]
        public int? lpr_PrintLabelID { get; set; }

    }
}