namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_DimensionGroup")]
    public class DimensionGroup
    {
        [Column("dgr_ID", true)] // Primary Key
        public int dgr_ID { get; set; }

        [Column("dgr_Code")]
        public string dgr_Code { get; set; }

        [Column("dgr_Description")]
        public string dgr_Description { get; set; }

        [Column("dgr_DomainID")]
        public int? dgr_DomainID { get; set; }

    }
}