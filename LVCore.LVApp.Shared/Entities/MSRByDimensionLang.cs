namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_MSRByDimensionLang")]
    public class MSRByDimensionLang
    {
        [Column("msbl_ID", true)] // Primary Key
        public int msbl_ID { get; set; }

        [Column("msbl_DimensionID")]
        public int? msbl_DimensionID { get; set; }

        [Column("msbl_Description")]
        public string msbl_Description { get; set; }

        [Column("msbl_LanguageID")]
        public int? msbl_LanguageID { get; set; }

        [Column("msbl_DomainID")]
        public int? msbl_DomainID { get; set; }

    }
}