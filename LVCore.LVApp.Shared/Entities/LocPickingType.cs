namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LocPickingType")]
    public class LocPickingType
    {
        [Column("lpk_ID", true)] // Primary Key
        public int lpk_ID { get; set; }

        [Column("lpk_Code")]
        public string lpk_Code { get; set; }

        [Column("lpk_MessageCode")]
        public string lpk_MessageCode { get; set; }

        [Column("lpk_PuttingPriority")]
        public int? lpk_PuttingPriority { get; set; }

        [Column("lpk_DomainID")]
        public int? lpk_DomainID { get; set; }

    }
}