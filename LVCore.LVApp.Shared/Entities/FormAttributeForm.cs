namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_FormAttributeForm")]
    public class FormAttributeForm
    {
        [Column("faf_ID", true)] // Primary Key
        public int faf_ID { get; set; }

        [Column("faf_ActionID")]
        public int? faf_ActionID { get; set; }

        [Column("faf_AttributeID")]
        public int? faf_AttributeID { get; set; }

        [Column("faf_DomainID")]
        public int? faf_DomainID { get; set; }

    }
}