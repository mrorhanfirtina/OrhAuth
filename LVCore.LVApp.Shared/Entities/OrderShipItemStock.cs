namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderShipItemStock")]
    public class OrderShipItemStock
    {
        [Column("oss_ID", true)] // Primary Key
        public int oss_ID { get; set; }

        [Column("oss_TaskID")]
        public int? oss_TaskID { get; set; }

        [Column("oss_OrderShipItemID")]
        public int? oss_OrderShipItemID { get; set; }

        [Column("oss_GroupItemID")]
        public int? oss_GroupItemID { get; set; }

        [Column("oss_OSIInvID")]
        public int? oss_OSIInvID { get; set; }

        [Column("oss_OSIInvExtID")]
        public int? oss_OSIInvExtID { get; set; }

        [Column("oss_TaskListID")]
        public int? oss_TaskListID { get; set; }

        [Column("oss_StockID")]
        public int? oss_StockID { get; set; }

        [Column("oss_StockPackTypeID")]
        public int? oss_StockPackTypeID { get; set; }

        [Column("oss_Quantity")]
        public decimal? oss_Quantity { get; set; }

        [Column("oss_QuantitySU")]
        public decimal? oss_QuantitySU { get; set; }

        [Column("oss_SUItemUnitID")]
        public int? oss_SUItemUnitID { get; set; }

        [Column("oss_ContainerID")]
        public int? oss_ContainerID { get; set; }

        [Column("oss_LogStockID")]
        public int? oss_LogStockID { get; set; }

        [Column("oss_LogStockPackTypeID")]
        public int? oss_LogStockPackTypeID { get; set; }

        [Column("oss_LogContainerID")]
        public int? oss_LogContainerID { get; set; }

        [Column("oss_ExpStockID")]
        public int? oss_ExpStockID { get; set; }

        [Column("oss_ExpStockPackTypeID")]
        public int? oss_ExpStockPackTypeID { get; set; }

        [Column("oss_ExpContainerID")]
        public int? oss_ExpContainerID { get; set; }

        [Column("oss_PickListLED")]
        public int? oss_PickListLED { get; set; }

        [Column("oss_PickedLED")]
        public int? oss_PickedLED { get; set; }

        [Column("oss_SortedLED")]
        public int? oss_SortedLED { get; set; }

        [Column("oss_PackedQty")]
        public decimal? oss_PackedQty { get; set; }

        [Column("oss_LoadedLED")]
        public int? oss_LoadedLED { get; set; }

        [Column("oss_DomainID")]
        public int oss_DomainID { get; set; }

        [Column("oss_CheckedLED")]
        public int? oss_CheckedLED { get; set; }

    }
}