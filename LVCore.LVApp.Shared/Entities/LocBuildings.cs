namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LocBuildings")]
    public class LocBuildings
    {
        [Column("lcb_ID", true)] // Primary Key
        public int lcb_ID { get; set; }

        [Column("lcb_Code")]
        public string lcb_Code { get; set; }

        [Column("lcb_Description")]
        public string lcb_Description { get; set; }

        [Column("lcb_LocCodeFormat")]
        public string lcb_LocCodeFormat { get; set; }

        [Column("lcb_LogisticSiteID")]
        public int lcb_LogisticSiteID { get; set; }

        [Column("lcb_InBufferLocationID")]
        public int? lcb_InBufferLocationID { get; set; }

        [Column("lcb_DomainID")]
        public int? lcb_DomainID { get; set; }

    }
}