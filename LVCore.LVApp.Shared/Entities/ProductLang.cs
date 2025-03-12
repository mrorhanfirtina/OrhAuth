namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ProductLang")]
    public class ProductLang
    {
        [Column("prdl_ID", true)] // Primary Key
        public int prdl_ID { get; set; }

        [Column("prdl_ProductID")]
        public int? prdl_ProductID { get; set; }

        [Column("prdl_Description")]
        public string prdl_Description { get; set; }

        [Column("prdl_ShortDescription")]
        public string prdl_ShortDescription { get; set; }

        [Column("prdl_LanguageID")]
        public int? prdl_LanguageID { get; set; }

        [Column("prdl_DomainID")]
        public int? prdl_DomainID { get; set; }

    }
}