namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_MissingStockAttrValue")]
    public class MissingStockAttrValue
    {
        [Column("msa_ID", true)] // Primary Key
        public int msa_ID { get; set; }

        [Column("msa_MissingStockLogID")]
        public int? msa_MissingStockLogID { get; set; }

        [Column("msa_AttributeID")]
        public int? msa_AttributeID { get; set; }

        [Column("msa_Value")]
        public string msa_Value { get; set; }

        [Column("msa_DomainID")]
        public int? msa_DomainID { get; set; }

    }
}