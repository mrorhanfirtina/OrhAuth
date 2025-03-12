namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ItemUnit")]
    public class ItemUnit
    {
        [Column("itu_ID", true)] // Primary Key
        public int itu_ID { get; set; }

        [Column("itu_ProductID")]
        public int? itu_ProductID { get; set; }

        [Column("itu_UnitID")]
        public int? itu_UnitID { get; set; }

        [Column("itu_UseLevelID")]
        public int? itu_UseLevelID { get; set; }

        [Column("itu_AlternateStockUnitLED")]
        public int? itu_AlternateStockUnitLED { get; set; }

        [Column("itu_RecordInTransTypeID")]
        public int? itu_RecordInTransTypeID { get; set; }

        [Column("itu_RecordPerUnitLED")]
        public int? itu_RecordPerUnitLED { get; set; }

        [Column("itu_ConsumerUnitLED")]
        public int? itu_ConsumerUnitLED { get; set; }

        [Column("itu_OrderUnitLED")]
        public int? itu_OrderUnitLED { get; set; }

        [Column("itu_ReceivingUnitLED")]
        public int? itu_ReceivingUnitLED { get; set; }

        [Column("itu_MinReceivingUnitLED")]
        public int? itu_MinReceivingUnitLED { get; set; }

        [Column("itu_SaleUnitLED")]
        public int? itu_SaleUnitLED { get; set; }

        [Column("itu_POrderUnitLED")]
        public int? itu_POrderUnitLED { get; set; }

        [Column("itu_BalanceLED")]
        public int? itu_BalanceLED { get; set; }

        [Column("itu_DomainID")]
        public int? itu_DomainID { get; set; }

        [Column("itu_ExportRatio")]
        public int? itu_ExportRatio { get; set; }

        [Column("itu_ExportUnit")]
        public string itu_ExportUnit { get; set; }

        [Column("itu_PickWholeLED")]
        public int? itu_PickWholeLED { get; set; }

        [Column("itu_KeepProductID")]
        public int? itu_KeepProductID { get; set; }

        [Column("itu_InputUnitLED")]
        public int? itu_InputUnitLED { get; set; }

        [Column("itu_PickFromStockLED")]
        public int? itu_PickFromStockLED { get; set; }

        [Column("itu_StockControlLED")]
        public int? itu_StockControlLED { get; set; }

        [Column("itu_VarUpperLimit")]
        public decimal? itu_VarUpperLimit { get; set; }

        [Column("itu_VarLowerLimit")]
        public decimal? itu_VarLowerLimit { get; set; }

        [Column("itu_VarPercentLED")]
        public int? itu_VarPercentLED { get; set; }

        [Column("itu_QtyNotDefaultedLED")]
        public int? itu_QtyNotDefaultedLED { get; set; }

        [Column("itu_ExceedOrderLimit")]
        public decimal? itu_ExceedOrderLimit { get; set; }

        [Column("itu_UseAsNetWeightLED")]
        public int? itu_UseAsNetWeightLED { get; set; }

        [Column("itu_VariableDeadWeightLED")]
        public int? itu_VariableDeadWeightLED { get; set; }

    }
}