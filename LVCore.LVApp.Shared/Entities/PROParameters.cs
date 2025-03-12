namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("PRO_Parameters")]
    public class PROParameters
    {
        [Column("pap_ID", true)] // Primary Key
        public int pap_ID { get; set; }

        [Column("pap_Type")]
        public int? pap_Type { get; set; }

        [Column("pap_Name")]
        public string pap_Name { get; set; }

        [Column("pap_Value")]
        public string pap_Value { get; set; }

        [Column("pap_DomainID")]
        public int? pap_DomainID { get; set; }

        [Column("pap_LogisticSiteID")]
        public int? pap_LogisticSiteID { get; set; }

    }
}