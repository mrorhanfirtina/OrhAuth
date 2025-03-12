namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_MissingStockLog")]
    public class MissingStockLog
    {
        [Column("msl_ID", true)] // Primary Key
        public int msl_ID { get; set; }

        [Column("msl_EmployeeID")]
        public int? msl_EmployeeID { get; set; }

        [Column("msl_Datetime")]
        public DateTime? msl_Datetime { get; set; }

        [Column("msl_OrderShipItemID")]
        public int? msl_OrderShipItemID { get; set; }

        [Column("msl_OrderShipGroupItemID")]
        public int? msl_OrderShipGroupItemID { get; set; }

        [Column("msl_LocationID")]
        public int? msl_LocationID { get; set; }

        [Column("msl_ProductID")]
        public int? msl_ProductID { get; set; }

        [Column("msl_SSCC")]
        public string msl_SSCC { get; set; }

        [Column("msl_ContainerUnitID")]
        public int? msl_ContainerUnitID { get; set; }

        [Column("msl_ItemUnitID")]
        public int? msl_ItemUnitID { get; set; }

        [Column("msl_Quantity")]
        public decimal? msl_Quantity { get; set; }

        [Column("msl_DomainID")]
        public int? msl_DomainID { get; set; }

        [Column("msl_ReserveReasonID")]
        public int? msl_ReserveReasonID { get; set; }

    }
}