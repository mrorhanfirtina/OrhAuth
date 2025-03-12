namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("SYS_TriggerOrigin")]
    public class SYSTriggerOrigin
    {
        [Column("tro_ID", true)] // Primary Key
        public int tro_ID { get; set; }

        [Column("tro_Code")]
        public int? tro_Code { get; set; }

        [Column("tro_MessageCode")]
        public string tro_MessageCode { get; set; }

        [Column("tro_DomainID")]
        public int? tro_DomainID { get; set; }

        [Column("tro_TableName")]
        public string tro_TableName { get; set; }

        [Column("tro_PrimaryKey")]
        public string tro_PrimaryKey { get; set; }

        [Column("tro_FormID")]
        public int? tro_FormID { get; set; }

    }
}