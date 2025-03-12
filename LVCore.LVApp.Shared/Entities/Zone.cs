namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Zone")]
    public class Zone
    {
        [Column("zon_ID", true)] // Primary Key
        public int zon_ID { get; set; }

        [Column("zon_code")]
        public string zon_code { get; set; }

        [Column("zon_Description")]
        public string zon_Description { get; set; }

        [Column("zon_DomainID")]
        public int? zon_DomainID { get; set; }

    }
}