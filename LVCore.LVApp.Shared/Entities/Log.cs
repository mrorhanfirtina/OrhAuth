namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Log")]
    public class Log
    {
        [Column("log_ID", true)] // Primary Key
        public int log_ID { get; set; }

        [Column("log_DateTime")]
        public DateTime? log_DateTime { get; set; }

        [Column("log_UserID")]
        public int? log_UserID { get; set; }

        [Column("log_SessionID")]
        public int? log_SessionID { get; set; }

        [Column("log_TransactionTypeID")]
        public int? log_TransactionTypeID { get; set; }

        [Column("log_ReceiptID")]
        public int? log_ReceiptID { get; set; }

        [Column("log_ReceiptItemID")]
        public int? log_ReceiptItemID { get; set; }

        [Column("log_ReturnID")]
        public int? log_ReturnID { get; set; }

        [Column("log_ReturnItemID")]
        public int? log_ReturnItemID { get; set; }

        [Column("log_OrderShipmentID")]
        public int? log_OrderShipmentID { get; set; }

        [Column("log_OrderShipItemID")]
        public int? log_OrderShipItemID { get; set; }

        [Column("log_OrderShipmentGroupID")]
        public int? log_OrderShipmentGroupID { get; set; }

        [Column("log_OrderShipGroupItemID")]
        public int? log_OrderShipGroupItemID { get; set; }

        [Column("log_ProductionOrderID")]
        public int? log_ProductionOrderID { get; set; }

        [Column("log_ProdItemByProdOrderID")]
        public int? log_ProdItemByProdOrderID { get; set; }

        [Column("log_TaskCode")]
        public string log_TaskCode { get; set; }

        [Column("log_ReasonCode")]
        public string log_ReasonCode { get; set; }

        [Column("log_DomainID")]
        public int? log_DomainID { get; set; }

        [Column("log_StartDateTime")]
        public DateTime? log_StartDateTime { get; set; }

        [Column("log_DocumentCreatedLED")]
        public int? log_DocumentCreatedLED { get; set; }

        [Column("log_ExportedLED")]
        public int? log_ExportedLED { get; set; }

        [Column("log_TotalDuration")]
        public decimal? log_TotalDuration { get; set; }

        [Column("log_UserDuration")]
        public decimal? log_UserDuration { get; set; }

        [Column("log_LogGroup")]
        public string log_LogGroup { get; set; }

        [Column("log_TaskID")]
        public int? log_TaskID { get; set; }

    }
}