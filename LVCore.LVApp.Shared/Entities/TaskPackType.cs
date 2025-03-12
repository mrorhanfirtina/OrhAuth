namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TaskPackType")]
    public class TaskPackType
    {
        [Column("tpt_ID", true)] // Primary Key
        public int tpt_ID { get; set; }

        [Column("tpt_TaskID")]
        public int? tpt_TaskID { get; set; }

        [Column("tpt_ItemUnitID")]
        public int? tpt_ItemUnitID { get; set; }

        [Column("tpt_Quantity")]
        public decimal? tpt_Quantity { get; set; }

        [Column("tpt_PackTypeRatio")]
        public decimal? tpt_PackTypeRatio { get; set; }

        [Column("tpt_ParentID")]
        public int? tpt_ParentID { get; set; }

        [Column("tpt_DomainID")]
        public int? tpt_DomainID { get; set; }

    }
}