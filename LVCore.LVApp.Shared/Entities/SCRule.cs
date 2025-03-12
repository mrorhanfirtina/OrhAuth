namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_SCRule")]
    public class SCRule
    {
        [Column("scr_ID", true)] // Primary Key
        public int scr_ID { get; set; }

        [Column("scr_Code")]
        public string scr_Code { get; set; }

        [Column("scr_Description")]
        public string scr_Description { get; set; }

        [Column("scr_Condition")]
        public string scr_Condition { get; set; }

        [Column("scr_UseInSoftSCLED")]
        public int? scr_UseInSoftSCLED { get; set; }

        [Column("scr_DomainID")]
        public int? scr_DomainID { get; set; }

        [Column("scr_SCFields")]
        public string scr_SCFields { get; set; }

        [Column("scr_SCFrom")]
        public string scr_SCFrom { get; set; }

        [Column("scr_SCWhere")]
        public string scr_SCWhere { get; set; }

        [Column("scr_SCGroupFields")]
        public string scr_SCGroupFields { get; set; }

    }
}