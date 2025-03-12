namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ProductAttributes")]
    public class ProductAttributes
    {
        [Column("pat_ID", true)] // Primary Key
        public int pat_ID { get; set; }

        [Column("pat_Code")]
        public string pat_Code { get; set; }

        [Column("pat_TypeID")]
        public int pat_TypeID { get; set; }

        [Column("pat_GroupID")]
        public int? pat_GroupID { get; set; }

        [Column("pat_DefaultValue")]
        public string pat_DefaultValue { get; set; }

        [Column("pat_DefaultCode")]
        public string pat_DefaultCode { get; set; }

        [Column("pat_ParentAttributeID")]
        public int? pat_ParentAttributeID { get; set; }

        [Column("pat_ParentRequiredLED")]
        public int? pat_ParentRequiredLED { get; set; }

        [Column("pat_Comments")]
        public string pat_Comments { get; set; }

        [Column("pat_RequiredLED")]
        public int? pat_RequiredLED { get; set; }

        [Column("pat_OrderCriterionLED")]
        public int? pat_OrderCriterionLED { get; set; }

        [Column("pat_OrderDetailLED")]
        public int? pat_OrderDetailLED { get; set; }

        [Column("pat_DomainID")]
        public int? pat_DomainID { get; set; }

        [Column("pat_RequiredPerDepositorLED")]
        public int? pat_RequiredPerDepositorLED { get; set; }

        [Column("pat_FillOnKeyPressLED")]
        public int? pat_FillOnKeyPressLED { get; set; }

        [Column("pat_MISIndex")]
        public int? pat_MISIndex { get; set; }

        [Column("pat_ReadOnlyLED")]
        public int? pat_ReadOnlyLED { get; set; }

    }
}