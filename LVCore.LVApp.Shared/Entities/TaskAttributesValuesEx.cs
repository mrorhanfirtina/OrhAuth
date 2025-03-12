namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TaskAttributesValuesEx")]
    public class TaskAttributesValuesEx
    {
        [Column("tax_ID", true)] // Primary Key
        public int tax_ID { get; set; }

        [Column("tax_TaskID")]
        public int? tax_TaskID { get; set; }

        [Column("tax_AttributeID")]
        public int? tax_AttributeID { get; set; }

        [Column("tax_Value")]
        public string tax_Value { get; set; }

        [Column("tax_DomainID")]
        public int? tax_DomainID { get; set; }

    }
}