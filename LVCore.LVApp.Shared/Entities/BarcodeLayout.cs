namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_BarcodeLayout")]
    public class BarcodeLayout
    {
        [Column("bal_ID", true)] // Primary Key
        public int bal_ID { get; set; }

        [Column("bal_Code")]
        public string bal_Code { get; set; }

        [Column("bal_Description")]
        public string bal_Description { get; set; }

        [Column("bal_UseAILED")]
        public int? bal_UseAILED { get; set; }

        [Column("bal_Pattern")]
        public string bal_Pattern { get; set; }

        [Column("bal_DomainID")]
        public int? bal_DomainID { get; set; }

        [Column("bal_ReplaceFormat")]
        public string bal_ReplaceFormat { get; set; }

    }
}