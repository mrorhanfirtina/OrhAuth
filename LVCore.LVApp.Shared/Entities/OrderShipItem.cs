namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_OrderShipItem")]
    public class OrderShipItem
    {
        [Column("osi_ID", true)] // Primary Key
        public int osi_ID { get; set; }

        [Column("osi_OrderShipmentID")]
        public int? osi_OrderShipmentID { get; set; }

        [Column("osi_OrderItemID")]
        public int? osi_OrderItemID { get; set; }

        [Column("osi_GroupItemID")]
        public int? osi_GroupItemID { get; set; }

        [Column("osi_Quantity")]
        public decimal? osi_Quantity { get; set; }

        [Column("osi_QuantitySU")]
        public decimal? osi_QuantitySU { get; set; }

        [Column("osi_Volume")]
        public decimal? osi_Volume { get; set; }

        [Column("osi_Weight")]
        public decimal? osi_Weight { get; set; }

        [Column("osi_VolumeUnitID")]
        public int? osi_VolumeUnitID { get; set; }

        [Column("osi_WeightUnitID")]
        public int? osi_WeightUnitID { get; set; }

        [Column("osi_StatusID")]
        public int? osi_StatusID { get; set; }

        [Column("osi_NoStockActionID")]
        public int? osi_NoStockActionID { get; set; }

        [Column("osi_Timestamp")]
        public DateTime? osi_Timestamp { get; set; }

        [Column("osi_LogisticUnitID")]
        public int? osi_LogisticUnitID { get; set; }

        [Column("osi_DomainID")]
        public int? osi_DomainID { get; set; }

        [Column("osi_LastProcessDate")]
        public DateTime? osi_LastProcessDate { get; set; }

    }
}