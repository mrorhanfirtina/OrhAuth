namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_SupplierAttributesValues")]
    public class SupplierAttributesValues
    {
        [Column("suv_ID", true)] // Primary Key
        public int suv_ID { get; set; }

        [Column("suv_SupplierID")]
        public int? suv_SupplierID { get; set; }

        [Column("suv_AttributeID")]
        public int? suv_AttributeID { get; set; }

        [Column("suv_value")]
        public string suv_value { get; set; }

        [Column("suv_DomainID")]
        public int? suv_DomainID { get; set; }

    }
}