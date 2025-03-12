namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_PrinterType")]
    public class PrinterType
    {
        [Column("prt_ID", true)] // Primary Key
        public int prt_ID { get; set; }

        [Column("prt_Description")]
        public string prt_Description { get; set; }

        [Column("prt_CodePage")]
        public string prt_CodePage { get; set; }

        [Column("prt_DomainID")]
        public int? prt_DomainID { get; set; }

        [Column("prt_PrinterTypeClassID")]
        public int? prt_PrinterTypeClassID { get; set; }

    }
}