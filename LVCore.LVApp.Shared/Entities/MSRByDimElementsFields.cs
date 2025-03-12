namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_MSRByDimElementsFields")]
    public class MSRByDimElementsFields
    {
        [Column("mdf_ID", true)] // Primary Key
        public int mdf_ID { get; set; }

        [Column("mdf_MSRByDimElementsID")]
        public int? mdf_MSRByDimElementsID { get; set; }

        [Column("mdf_FieldName")]
        public string mdf_FieldName { get; set; }

        [Column("mdf_DisplayLed")]
        public int? mdf_DisplayLed { get; set; }

        [Column("mdf_PriceListLed")]
        public int? mdf_PriceListLed { get; set; }

        [Column("mdf_ActiveDateLed")]
        public int? mdf_ActiveDateLed { get; set; }

        [Column("mdf_DomainID")]
        public int? mdf_DomainID { get; set; }

        [Column("mdf_ColHeader")]
        public string mdf_ColHeader { get; set; }

    }
}