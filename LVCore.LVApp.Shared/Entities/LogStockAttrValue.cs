namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LogStockAttrValue")]
    public class LogStockAttrValue
    {
        [Column("lsa_ID", true)] // Primary Key
        public int lsa_ID { get; set; }

        [Column("lsa_LogStockID")]
        public int? lsa_LogStockID { get; set; }

        [Column("lsa_AttributeID")]
        public int? lsa_AttributeID { get; set; }

        [Column("lsa_Value")]
        public string lsa_Value { get; set; }

        [Column("lsa_DomainID")]
        public int? lsa_DomainID { get; set; }

    }
}