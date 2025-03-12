namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_SelectFilters")]
    public class SelectFilters
    {
        [Column("sft_ID", true)] // Primary Key
        public int sft_ID { get; set; }

        [Column("sft_Code")]
        public string sft_Code { get; set; }

        [Column("sft_Description")]
        public string sft_Description { get; set; }

        [Column("sft_FieldTypeID")]
        public int? sft_FieldTypeID { get; set; }

        [Column("sft_Size")]
        public int? sft_Size { get; set; }

        [Column("sft_SQLCommand")]
        public string sft_SQLCommand { get; set; }

        [Column("sft_Format")]
        public string sft_Format { get; set; }

        [Column("sft_DomainID")]
        public int? sft_DomainID { get; set; }

    }
}