namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TaskPriorityLang")]
    public class TaskPriorityLang
    {
        [Column("tpl_ID", true)] // Primary Key
        public int tpl_ID { get; set; }

        [Column("tpl_TaskPriorityID")]
        public int? tpl_TaskPriorityID { get; set; }

        [Column("tpl_LanguageID")]
        public int? tpl_LanguageID { get; set; }

        [Column("tpl_Description")]
        public string tpl_Description { get; set; }

        [Column("tpl_DomainID")]
        public int? tpl_DomainID { get; set; }

    }
}