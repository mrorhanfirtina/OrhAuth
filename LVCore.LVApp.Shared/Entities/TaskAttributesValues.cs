namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TaskAttributesValues")]
    public class TaskAttributesValues
    {
        [Column("tav_ID", true)] // Primary Key
        public int tav_ID { get; set; }

        [Column("tav_TaskID")]
        public int? tav_TaskID { get; set; }

        [Column("tav_AttributeID")]
        public int? tav_AttributeID { get; set; }

        [Column("tav_Value")]
        public string tav_Value { get; set; }

        [Column("tav_DomainID")]
        public int? tav_DomainID { get; set; }

    }
}