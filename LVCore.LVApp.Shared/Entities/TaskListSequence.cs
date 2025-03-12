namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TaskListSequence")]
    public class TaskListSequence
    {
        [Column("tls_ID", true)] // Primary Key
        public int tls_ID { get; set; }

        [Column("tls_TypeID")]
        public int? tls_TypeID { get; set; }

        [Column("tls_Prefix")]
        public string tls_Prefix { get; set; }

        [Column("tls_Format")]
        public string tls_Format { get; set; }

        [Column("tls_ResetDays")]
        public int? tls_ResetDays { get; set; }

        [Column("tls_ResetValue")]
        public string tls_ResetValue { get; set; }

        [Column("tls_Value")]
        public int? tls_Value { get; set; }

        [Column("tls_LogisticSiteID")]
        public int? tls_LogisticSiteID { get; set; }

        [Column("tls_DomainID")]
        public int? tls_DomainID { get; set; }

    }
}