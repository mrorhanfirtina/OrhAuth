namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Host")]
    public class Host
    {
        [Column("Hst_ID", true)] // Primary Key
        public int Hst_ID { get; set; }

        [Column("hst_TypeID")]
        public int? hst_TypeID { get; set; }

        [Column("hst_Name")]
        public string hst_Name { get; set; }

        [Column("hst_IP")]
        public string hst_IP { get; set; }

        [Column("hst_DomainID")]
        public int hst_DomainID { get; set; }

        [Column("hst_PackLabelPrinter")]
        public string hst_PackLabelPrinter { get; set; }

        [Column("hst_PackLabelID")]
        public int? hst_PackLabelID { get; set; }

        [Column("hst_TerminalPointID")]
        public int? hst_TerminalPointID { get; set; }

    }
}