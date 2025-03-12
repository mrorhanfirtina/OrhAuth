namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LocZone")]
    public class LocZone
    {
        [Column("lzn_ID", true)] // Primary Key
        public int lzn_ID { get; set; }

        [Column("lzn_LocationID")]
        public int? lzn_LocationID { get; set; }

        [Column("lzn_ZoneID")]
        public int? lzn_ZoneID { get; set; }

        [Column("lzn_DomainID")]
        public int? lzn_DomainID { get; set; }

    }
}