namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorAttributeValues")]
    public class DepositorAttributeValues
    {
        [Column("dev_ID", true)] // Primary Key
        public int dev_ID { get; set; }

        [Column("dev_DepositorID")]
        public int? dev_DepositorID { get; set; }

        [Column("dev_AttributeID")]
        public int? dev_AttributeID { get; set; }

        [Column("dev_value")]
        public string dev_value { get; set; }

        [Column("dev_DomainID")]
        public int? dev_DomainID { get; set; }

    }
}