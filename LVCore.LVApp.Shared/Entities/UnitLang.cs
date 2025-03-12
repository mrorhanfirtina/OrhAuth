namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UnitLang")]
    public class UnitLang
    {
        [Column("untl_ID", true)] // Primary Key
        public int untl_ID { get; set; }

        [Column("untl_UnitID")]
        public int? untl_UnitID { get; set; }

        [Column("untl_Description")]
        public string untl_Description { get; set; }

        [Column("untl_ShortDescription")]
        public string untl_ShortDescription { get; set; }

        [Column("untl_LanguageID")]
        public int? untl_LanguageID { get; set; }

        [Column("untl_DomainID")]
        public int? untl_DomainID { get; set; }

    }
}