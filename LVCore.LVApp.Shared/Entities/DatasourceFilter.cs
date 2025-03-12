namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_DatasourceFilter")]
    public class DatasourceFilter
    {
        [Column("dfl_ID", true)] // Primary Key
        public int dfl_ID { get; set; }

        [Column("dfl_Code")]
        public string dfl_Code { get; set; }

        [Column("dfl_Description")]
        public string dfl_Description { get; set; }

        [Column("dfl_FieldTypeID")]
        public int? dfl_FieldTypeID { get; set; }

        [Column("dfl_Size")]
        public int? dfl_Size { get; set; }

        [Column("dfl_SQLCommand")]
        public string dfl_SQLCommand { get; set; }

        [Column("dfl_Format")]
        public string dfl_Format { get; set; }

        [Column("dfl_MessageCode")]
        public string dfl_MessageCode { get; set; }

        [Column("dfl_Label")]
        public string dfl_Label { get; set; }

        [Column("dfl_DomainID")]
        public int? dfl_DomainID { get; set; }

        [Column("dfl_DisplayOrder")]
        public int? dfl_DisplayOrder { get; set; }

    }
}