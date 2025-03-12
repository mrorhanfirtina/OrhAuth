namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_DashboardLang")]
    public class DashboardLang
    {
        [Column("dbdl_ID", true)] // Primary Key
        public int dbdl_ID { get; set; }

        [Column("dbdl_DashboardID")]
        public int? dbdl_DashboardID { get; set; }

        [Column("dbdl_LanguageID")]
        public int? dbdl_LanguageID { get; set; }

        [Column("dbdl_Description")]
        public string dbdl_Description { get; set; }

        [Column("dbdl_DomainID")]
        public int? dbdl_DomainID { get; set; }

    }
}