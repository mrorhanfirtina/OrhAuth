namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_CustomerAttributeValues")]
    public class CustomerAttributeValues
    {
        [Column("ctv_ID", true)] // Primary Key
        public int ctv_ID { get; set; }

        [Column("ctv_CustomerID")]
        public int? ctv_CustomerID { get; set; }

        [Column("ctv_AttributeID")]
        public int? ctv_AttributeID { get; set; }

        [Column("ctv_value")]
        public string ctv_value { get; set; }

        [Column("ctv_DomainID")]
        public int? ctv_DomainID { get; set; }

    }
}