namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderAttributeList")]
    public class OrderAttributeList
    {
        [Column("oal_ID", true)] // Primary Key
        public int oal_ID { get; set; }

        [Column("oal_AttributeID")]
        public int oal_AttributeID { get; set; }

        [Column("oal_Code")]
        public string oal_Code { get; set; }

        [Column("oal_DomainID")]
        public int? oal_DomainID { get; set; }

    }
}