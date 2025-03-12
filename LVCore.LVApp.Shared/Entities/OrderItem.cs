namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderItem")]
    public class OrderItem
    {
        [Column("ori_ID", true)] // Primary Key
        public int ori_ID { get; set; }

        [Column("ori_OrderID")]
        public int? ori_OrderID { get; set; }

        [Column("ori_ProductID")]
        public int? ori_ProductID { get; set; }

        [Column("ori_SSCC")]
        public string ori_SSCC { get; set; }

        [Column("ori_ItemUnitID")]
        public int? ori_ItemUnitID { get; set; }

        [Column("ori_Quantity")]
        public decimal? ori_Quantity { get; set; }

        [Column("ori_QuantitySU")]
        public decimal? ori_QuantitySU { get; set; }

        [Column("ori_SUItemUnitID")]
        public int? ori_SUItemUnitID { get; set; }

        [Column("ori_CancelReasonID")]
        public int? ori_CancelReasonID { get; set; }

        [Column("ori_AddedItemLED")]
        public int? ori_AddedItemLED { get; set; }

        [Column("ori_OrderLine")]
        public int? ori_OrderLine { get; set; }

        [Column("ori_RepeatSCLED")]
        public int? ori_RepeatSCLED { get; set; }

        [Column("ori_Volume")]
        public decimal? ori_Volume { get; set; }

        [Column("ori_VolumeUnitID")]
        public int? ori_VolumeUnitID { get; set; }

        [Column("ori_Weight")]
        public decimal? ori_Weight { get; set; }

        [Column("ori_WeightUnitID")]
        public int? ori_WeightUnitID { get; set; }

        [Column("ori_DomainID")]
        public int? ori_DomainID { get; set; }

        [Column("ori_SCRuleID")]
        public int? ori_SCRuleID { get; set; }

        [Column("ori_ParentLineID")]
        public int? ori_ParentLineID { get; set; }

        [Column("ori_KitVariantID")]
        public int? ori_KitVariantID { get; set; }

        [Column("ori_LogisticUnitID")]
        public int? ori_LogisticUnitID { get; set; }

        [Column("ori_Priority")]
        public int? ori_Priority { get; set; }

        [Column("ori_GroupReference")]
        public string ori_GroupReference { get; set; }

    }
}