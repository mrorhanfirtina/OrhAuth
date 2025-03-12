namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_DatasourceField")]
    public class DatasourceField
    {
        [Column("dsf_ID", true)] // Primary Key
        public int dsf_ID { get; set; }

        [Column("dsf_DatasourceID")]
        public int? dsf_DatasourceID { get; set; }

        [Column("dsf_FieldName")]
        public string dsf_FieldName { get; set; }

        [Column("dsf_FieldTypeLED")]
        public int? dsf_FieldTypeLED { get; set; }

        [Column("dsf_DomainID")]
        public int? dsf_DomainID { get; set; }

    }
}