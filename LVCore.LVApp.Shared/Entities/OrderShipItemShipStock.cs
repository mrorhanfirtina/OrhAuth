namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderShipItemShipStock")]
    public class OrderShipItemShipStock
    {
        [Column("osk_ID", true)] // Primary Key
        public int osk_ID { get; set; }

        [Column("osk_OrderShipItemID")]
        public int? osk_OrderShipItemID { get; set; }

        [Column("osk_GroupItemID")]
        public int? osk_GroupItemID { get; set; }

        [Column("osk_ShipStockID")]
        public int? osk_ShipStockID { get; set; }

        [Column("osk_ShipStockPackTypeID")]
        public int? osk_ShipStockPackTypeID { get; set; }

        [Column("osk_Quantity")]
        public decimal? osk_Quantity { get; set; }

        [Column("osk_ContainerID")]
        public int? osk_ContainerID { get; set; }

        [Column("osk_DomainID")]
        public int osk_DomainID { get; set; }

        [Column("osk_QuantitySU")]
        public decimal? osk_QuantitySU { get; set; }

        [Column("osk_SUItemUnitID")]
        public int? osk_SUItemUnitID { get; set; }

    }
}