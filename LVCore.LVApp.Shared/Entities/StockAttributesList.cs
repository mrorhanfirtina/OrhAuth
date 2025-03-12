namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_StockAttributesList")]
    public class StockAttributesList
    {
        [Column("sal_ID", true)] // Primary Key
        public int sal_ID { get; set; }

        [Column("sal_AttributeID")]
        public int? sal_AttributeID { get; set; }

        [Column("sal_Code")]
        public string sal_Code { get; set; }

        [Column("sal_DomainID")]
        public int sal_DomainID { get; set; }

    }
}