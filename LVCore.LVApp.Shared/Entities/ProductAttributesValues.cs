namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ProductAttributesValues")]
    public class ProductAttributesValues
    {
        [Column("pav_ID", true)] // Primary Key
        public int pav_ID { get; set; }

        [Column("pav_ProductID")]
        public int? pav_ProductID { get; set; }

        [Column("pav_attributeID")]
        public int? pav_attributeID { get; set; }

        [Column("pav_Value")]
        public string pav_Value { get; set; }

        [Column("pav_Order")]
        public int? pav_Order { get; set; }

        [Column("pav_RequiredForStorageLed")]
        public int? pav_RequiredForStorageLed { get; set; }

        [Column("pav_DomainID")]
        public int? pav_DomainID { get; set; }

        [Column("pav_NotForPuttingLED")]
        public int? pav_NotForPuttingLED { get; set; }

        [Column("pav_LogisticSiteID")]
        public int? pav_LogisticSiteID { get; set; }

    }
}