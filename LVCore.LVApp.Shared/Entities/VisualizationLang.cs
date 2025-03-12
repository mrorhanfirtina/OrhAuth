namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_VisualizationLang")]
    public class VisualizationLang
    {
        [Column("visl_ID", true)] // Primary Key
        public int visl_ID { get; set; }

        [Column("visl_VisualizationID")]
        public int? visl_VisualizationID { get; set; }

        [Column("visl_LanguageID")]
        public int? visl_LanguageID { get; set; }

        [Column("visl_Description")]
        public string visl_Description { get; set; }

        [Column("visl_DomainID")]
        public int? visl_DomainID { get; set; }

    }
}