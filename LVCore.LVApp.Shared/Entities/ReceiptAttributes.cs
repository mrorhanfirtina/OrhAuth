namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ReceiptAttributes")]
    public class ReceiptAttributes
    {
        [Column("rat_ID", true)] // Primary Key
        public int rat_ID { get; set; }

        [Column("rat_Code")]
        public string rat_Code { get; set; }

        [Column("rat_TypeID")]
        public int? rat_TypeID { get; set; }

        [Column("rat_DefaultValue")]
        public string rat_DefaultValue { get; set; }

        [Column("rat_DefaultCode")]
        public string rat_DefaultCode { get; set; }

        [Column("rat_Comments")]
        public string rat_Comments { get; set; }

        [Column("rat_DomainID")]
        public int? rat_DomainID { get; set; }

        [Column("rat_DetailOnlyLED")]
        public int? rat_DetailOnlyLED { get; set; }

        [Column("rat_DisplayPriority")]
        public int? rat_DisplayPriority { get; set; }

        [Column("rat_UseCode")]
        public int? rat_UseCode { get; set; }

        [Column("rat_UseInMasterPerDepLED")]
        public int? rat_UseInMasterPerDepLED { get; set; }

        [Column("rat_ReadOnlyLED")]
        public int? rat_ReadOnlyLED { get; set; }

        [Column("rat_RequiredLED")]
        public int? rat_RequiredLED { get; set; }

        [Column("rat_MISIndexMaster")]
        public int? rat_MISIndexMaster { get; set; }

        [Column("rat_MISIndexDetail")]
        public int? rat_MISIndexDetail { get; set; }

        [Column("rat_ModifyCompletedLED")]
        public int? rat_ModifyCompletedLED { get; set; }

    }
}