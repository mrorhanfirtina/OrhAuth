namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_CustomLabel")]
    public class CustomLabel
    {
        [Column("clb_ID", true)] // Primary Key
        public int clb_ID { get; set; }

        [Column("clb_PrintLabelID")]
        public int? clb_PrintLabelID { get; set; }

        [Column("clb_Select")]
        public string clb_Select { get; set; }

        [Column("clb_DomainID")]
        public int? clb_DomainID { get; set; }

    }
}