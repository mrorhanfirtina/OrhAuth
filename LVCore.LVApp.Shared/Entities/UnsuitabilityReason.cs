namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_UnsuitabilityReason")]
    public class UnsuitabilityReason
    {
        [Column("unr_ID", true)] // Primary Key
        public int unr_ID { get; set; }

        [Column("unr_Code")]
        public string unr_Code { get; set; }

        [Column("unr_Description")]
        public string unr_Description { get; set; }

        [Column("unr_DomainID")]
        public int? unr_DomainID { get; set; }

    }
}