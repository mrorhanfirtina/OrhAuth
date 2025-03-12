namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_PriceListLang")]
    public class PriceListLang
    {
        [Column("pcll_ID", true)] // Primary Key
        public int pcll_ID { get; set; }

        [Column("pcll_PriceListID")]
        public int? pcll_PriceListID { get; set; }

        [Column("pcll_Description")]
        public string pcll_Description { get; set; }

        [Column("pcll_LanguageID")]
        public int? pcll_LanguageID { get; set; }

        [Column("pcll_DomainID")]
        public int? pcll_DomainID { get; set; }

    }
}