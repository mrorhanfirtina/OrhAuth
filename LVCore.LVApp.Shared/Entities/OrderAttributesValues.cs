namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderAttributesValues")]
    public class OrderAttributesValues
    {
        [Column("oav_ID", true)] // Primary Key
        public int oav_ID { get; set; }

        [Column("oav_OrderID")]
        public int? oav_OrderID { get; set; }

        [Column("oav_AttributeID")]
        public int? oav_AttributeID { get; set; }

        [Column("oav_Code")]
        public string oav_Code { get; set; }

        [Column("oav_Value")]
        public string oav_Value { get; set; }

        [Column("oav_DomainID")]
        public int? oav_DomainID { get; set; }

    }
}