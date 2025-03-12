namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ImportIntItem")]
    public class ImportIntItem
    {
        [Column("iii_Id", true)] // Primary Key
        public int iii_Id { get; set; }

        [Column("iii_ImportID")]
        public int? iii_ImportID { get; set; }

        [Column("iii_FldName")]
        public string iii_FldName { get; set; }

        [Column("iii_FldIndex")]
        public int? iii_FldIndex { get; set; }

        [Column("iii_FldAttrCode")]
        public string iii_FldAttrCode { get; set; }

        [Column("iii_FldSize")]
        public int? iii_FldSize { get; set; }

        [Column("iii_FldValue")]
        public string iii_FldValue { get; set; }

        [Column("iii_DefValue")]
        public string iii_DefValue { get; set; }

        [Column("iii_SQL")]
        public string iii_SQL { get; set; }

        [Column("iii_SkipRecOnNull")]
        public int? iii_SkipRecOnNull { get; set; }

        [Column("iii_AddForeignRecordLED")]
        public int? iii_AddForeignRecordLED { get; set; }

        [Column("iii_DomainID")]
        public int? iii_DomainID { get; set; }

    }
}