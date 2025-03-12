namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_NoStockAction")]
    public class NoStockAction
    {
        [Column("nsa_ID", true)] // Primary Key
        public int nsa_ID { get; set; }

        [Column("nsa_Code")]
        public string nsa_Code { get; set; }

        [Column("nsa_MessageCode")]
        public string nsa_MessageCode { get; set; }

        [Column("nsa_DomainID")]
        public int? nsa_DomainID { get; set; }

    }
}