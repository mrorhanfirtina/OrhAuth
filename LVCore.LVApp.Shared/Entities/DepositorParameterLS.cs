namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorParameterLS")]
    public class DepositorParameterLS
    {
        [Column("dpl_ID", true)] // Primary Key
        public int dpl_ID { get; set; }

        [Column("dpl_DepositorID")]
        public int? dpl_DepositorID { get; set; }

        [Column("dpl_LogisticSiteID")]
        public int? dpl_LogisticSiteID { get; set; }

        [Column("dpl_ReceiptLocationID")]
        public int? dpl_ReceiptLocationID { get; set; }

        [Column("dpl_PickingLocationID")]
        public int? dpl_PickingLocationID { get; set; }

        [Column("dpl_DomainID")]
        public int? dpl_DomainID { get; set; }

        [Column("dpl_LeavePickingPriorityLED")]
        public int? dpl_LeavePickingPriorityLED { get; set; }

        [Column("dpl_ReturnLocationID")]
        public int? dpl_ReturnLocationID { get; set; }

        [Column("dpl_PutawayScenarioID")]
        public int? dpl_PutawayScenarioID { get; set; }

        [Column("dpl_FindPutLocOnCreationLED")]
        public int? dpl_FindPutLocOnCreationLED { get; set; }

    }
}