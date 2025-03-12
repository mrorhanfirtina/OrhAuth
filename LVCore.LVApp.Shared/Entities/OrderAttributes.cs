namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderAttributes")]
    public class OrderAttributes
    {
        [Column("oat_ID", true)] // Primary Key
        public int oat_ID { get; set; }

        [Column("oat_Code")]
        public string oat_Code { get; set; }

        [Column("oat_TypeID")]
        public int? oat_TypeID { get; set; }

        [Column("oat_DefaultValue")]
        public string oat_DefaultValue { get; set; }

        [Column("oat_DefaultCode")]
        public string oat_DefaultCode { get; set; }

        [Column("oat_Comments")]
        public string oat_Comments { get; set; }

        [Column("oat_DomainID")]
        public int? oat_DomainID { get; set; }

        [Column("oat_DetailOnlyLED")]
        public int? oat_DetailOnlyLED { get; set; }

        [Column("oat_DisplayPriority")]
        public int? oat_DisplayPriority { get; set; }

        [Column("oat_UseInMasterPerDepLED")]
        public int? oat_UseInMasterPerDepLED { get; set; }

        [Column("oat_ReadOnlyLED")]
        public int? oat_ReadOnlyLED { get; set; }

        [Column("oat_RequiredLED")]
        public int? oat_RequiredLED { get; set; }

        [Column("oat_ModifyCompletedLED")]
        public int? oat_ModifyCompletedLED { get; set; }

        [Column("oat_MISIndexMaster")]
        public int? oat_MISIndexMaster { get; set; }

        [Column("oat_MISIndexDetail")]
        public int? oat_MISIndexDetail { get; set; }

    }
}