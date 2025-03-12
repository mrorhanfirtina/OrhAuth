namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_MeasurementLang")]
    public class MeasurementLang
    {
        [Column("msrl_ID", true)] // Primary Key
        public int msrl_ID { get; set; }

        [Column("msrl_MeasurementID")]
        public int? msrl_MeasurementID { get; set; }

        [Column("msrl_Description")]
        public string msrl_Description { get; set; }

        [Column("msrl_LanguageID")]
        public int? msrl_LanguageID { get; set; }

        [Column("msrl_DomainID")]
        public int? msrl_DomainID { get; set; }

    }
}