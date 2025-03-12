namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_SequencesLS")]
    public class SequencesLS
    {
        [Column("sql_ID", true)] // Primary Key
        public int sql_ID { get; set; }

        [Column("sql_FieldName")]
        public string sql_FieldName { get; set; }

        [Column("sql_Value")]
        public int? sql_Value { get; set; }

        [Column("sql_Table")]
        public string sql_Table { get; set; }

        [Column("sql_Prefix")]
        public string sql_Prefix { get; set; }

        [Column("sql_Format")]
        public string sql_Format { get; set; }

        [Column("sql_MessageCode")]
        public string sql_MessageCode { get; set; }

        [Column("sql_LogisticSiteID")]
        public int? sql_LogisticSiteID { get; set; }

        [Column("sql_ResetBase")]
        public string sql_ResetBase { get; set; }

        [Column("sql_ResetValue")]
        public string sql_ResetValue { get; set; }

    }
}