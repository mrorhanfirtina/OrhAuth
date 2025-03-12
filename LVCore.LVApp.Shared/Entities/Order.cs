namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Order")]
    public class Order
    {
        [Column("ord_ID", true)] // Primary Key
        public int ord_ID { get; set; }

        [Column("ord_Code")]
        public string ord_Code { get; set; }

        [Column("ord_ProductionOrderID")]
        public int? ord_ProductionOrderID { get; set; }

        [Column("ord_CustomerOrderCode")]
        public string ord_CustomerOrderCode { get; set; }

        [Column("ord_InputDate")]
        public DateTime? ord_InputDate { get; set; }

        [Column("ord_DepositorID")]
        public int? ord_DepositorID { get; set; }

        [Column("ord_DepositorDocument")]
        public string ord_DepositorDocument { get; set; }

        [Column("ord_TypeID")]
        public int? ord_TypeID { get; set; }

        [Column("ord_BackOrderLED")]
        public int? ord_BackOrderLED { get; set; }

        [Column("ord_SegmentLED")]
        public int? ord_SegmentLED { get; set; }

        [Column("ord_OldOrderID")]
        public int? ord_OldOrderID { get; set; }

        [Column("ord_CustomerID")]
        public int? ord_CustomerID { get; set; }

        [Column("ord_SupplierID")]
        public int? ord_SupplierID { get; set; }

        [Column("ord_ReceivingCustomerID")]
        public int? ord_ReceivingCustomerID { get; set; }

        [Column("ord_ReceivingBranchID")]
        public int? ord_ReceivingBranchID { get; set; }

        [Column("ord_StatusID")]
        public int? ord_StatusID { get; set; }

        [Column("ord_ExpExecuteDate")]
        public DateTime? ord_ExpExecuteDate { get; set; }

        [Column("ord_ExpShipDate")]
        public DateTime? ord_ExpShipDate { get; set; }

        [Column("ord_ExpDeliveryDate")]
        public DateTime? ord_ExpDeliveryDate { get; set; }

        [Column("ord_Priority")]
        public int? ord_Priority { get; set; }

        [Column("ord_ReceiptID")]
        public int? ord_ReceiptID { get; set; }

        [Column("ord_DepartmentID")]
        public int? ord_DepartmentID { get; set; }

        [Column("ord_ContactedLED")]
        public int? ord_ContactedLED { get; set; }

        [Column("ord_Value")]
        public decimal? ord_Value { get; set; }

        [Column("ord_Memo")]
        public string ord_Memo { get; set; }

        [Column("ord_ShipSeq")]
        public int? ord_ShipSeq { get; set; }

        [Column("ord_DispatchMethodID")]
        public int? ord_DispatchMethodID { get; set; }

        [Column("ord_AgencyID")]
        public int? ord_AgencyID { get; set; }

        [Column("ord_Timestamp")]
        public DateTime? ord_Timestamp { get; set; }

        [Column("ord_LogisticSiteID")]
        public int? ord_LogisticSiteID { get; set; }

        [Column("ord_DomainID")]
        public int? ord_DomainID { get; set; }

        [Column("ord_ExportedLED")]
        public int? ord_ExportedLED { get; set; }

        [Column("ord_ToDepositorID")]
        public int? ord_ToDepositorID { get; set; }

        [Column("ord_OriginalOrderCode")]
        public string ord_OriginalOrderCode { get; set; }

        [Column("ord_BackOrderCounter")]
        public int? ord_BackOrderCounter { get; set; }

        [Column("ord_AgencyZoneID")]
        public int? ord_AgencyZoneID { get; set; }

        [Column("ord_CreateUserID")]
        public int? ord_CreateUserID { get; set; }

        [Column("ord_WorkOrderID")]
        public int? ord_WorkOrderID { get; set; }

        [Column("ord_BilledLED")]
        public int? ord_BilledLED { get; set; }

        [Column("ord_DeliveryLocationID")]
        public int? ord_DeliveryLocationID { get; set; }

        [Column("ord_DeliveryLUID")]
        public int? ord_DeliveryLUID { get; set; }

        [Column("ord_LastUpdateTime")]
        public DateTime? ord_LastUpdateTime { get; set; }

        [Column("ord_AgencyServiceID")]
        public int? ord_AgencyServiceID { get; set; }

        [Column("ord_LineCount")]
        public int? ord_LineCount { get; set; }

    }
}