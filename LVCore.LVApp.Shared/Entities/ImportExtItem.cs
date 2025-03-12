namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ImportExtItem")]
    public class ImportExtItem
    {
        [Column("iei_ID", true)] // Primary Key
        public int iei_ID { get; set; }

        [Column("iei_ImportID")]
        public int? iei_ImportID { get; set; }

        [Column("iei_Row")]
        public int? iei_Row { get; set; }

        [Column("iei_SubRecCode")]
        public string iei_SubRecCode { get; set; }

        [Column("iei_FromColumn")]
        public int? iei_FromColumn { get; set; }

        [Column("iei_Size")]
        public int? iei_Size { get; set; }

        [Column("iei_Order")]
        public int? iei_Order { get; set; }

        [Column("iei_FieldTag")]
        public string iei_FieldTag { get; set; }

        [Column("iei_DefValue")]
        public string iei_DefValue { get; set; }

        [Column("iei_FindStr")]
        public string iei_FindStr { get; set; }

        [Column("iei_ReplaceStr")]
        public string iei_ReplaceStr { get; set; }

        [Column("iei_Lookup")]
        public object iei_Lookup { get; set; }

        [Column("iei_CacheSQLLED")]
        public int? iei_CacheSQLLED { get; set; }

        [Column("iei_SQL")]
        public string iei_SQL { get; set; }

        [Column("iei_Convert")]
        public string iei_Convert { get; set; }

        [Column("iei_DomainID")]
        public int? iei_DomainID { get; set; }

        [Column("iei_XMLpath")]
        public string iei_XMLpath { get; set; }

        [Column("iei_XMLQualTag")]
        public string iei_XMLQualTag { get; set; }

        [Column("iei_XMLQualTagValue")]
        public string iei_XMLQualTagValue { get; set; }

        [Column("iei_MultipleLED")]
        public int? iei_MultipleLED { get; set; }

    }
}