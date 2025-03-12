namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Country")]
    public class Country
    {
        [Column("cty_ID", true)] // Primary Key
        public int cty_ID { get; set; }

        [Column("cty_Code")]
        public string cty_Code { get; set; }

        [Column("cty_Description")]
        public string cty_Description { get; set; }

        [Column("cty_DescriptionIntl")]
        public string cty_DescriptionIntl { get; set; }

        [Column("cty_DomainID")]
        public int? cty_DomainID { get; set; }

        [Column("cty_ISOCode2")]
        public string cty_ISOCode2 { get; set; }

        [Column("cty_ISOCode3")]
        public string cty_ISOCode3 { get; set; }

        [Column("cty_ISONumCode")]
        public string cty_ISONumCode { get; set; }

        [Column("cty_CurrencyID")]
        public int? cty_CurrencyID { get; set; }

        [Column("cty_NAFTAMemberLED")]
        public int? cty_NAFTAMemberLED { get; set; }

    }
}