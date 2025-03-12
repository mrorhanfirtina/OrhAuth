namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Receipt")]
    public class Receipt
    {
        [Column("rct_ID", true)] // Primary Key
        public int rct_ID { get; set; }

        [Column("rct_Code")]
        public string rct_Code { get; set; }

        [Column("rct_PurchaseOrderID")]
        public int? rct_PurchaseOrderID { get; set; }

        [Column("rct_ProgressID")]
        public int? rct_ProgressID { get; set; }

        [Column("rct_DepositorID")]
        public int? rct_DepositorID { get; set; }

        [Column("rct_SupplierID")]
        public int? rct_SupplierID { get; set; }

        [Column("rct_TypeID")]
        public int? rct_TypeID { get; set; }

        [Column("rct_ProductionOrderID")]
        public int? rct_ProductionOrderID { get; set; }

        [Column("rct_InputDate")]
        public DateTime? rct_InputDate { get; set; }

        [Column("rct_ExpectedDate")]
        public DateTime? rct_ExpectedDate { get; set; }

        [Column("rct_ExpectedDuration")]
        public int? rct_ExpectedDuration { get; set; }

        [Column("rct_ActualDate")]
        public DateTime? rct_ActualDate { get; set; }

        [Column("rct_ActualDuration")]
        public int? rct_ActualDuration { get; set; }

        [Column("rct_RetentionLED")]
        public int? rct_RetentionLED { get; set; }

        [Column("rct_ExpVolume")]
        public decimal? rct_ExpVolume { get; set; }

        [Column("rct_ExpNetWeight")]
        public decimal? rct_ExpNetWeight { get; set; }

        [Column("rct_ExpGrossWeight")]
        public decimal? rct_ExpGrossWeight { get; set; }

        [Column("rct_Volume")]
        public decimal? rct_Volume { get; set; }

        [Column("rct_VolumeUnitID")]
        public int? rct_VolumeUnitID { get; set; }

        [Column("rct_GrossWeight")]
        public decimal? rct_GrossWeight { get; set; }

        [Column("rct_NetWeight")]
        public decimal? rct_NetWeight { get; set; }

        [Column("rct_WeightUnitID")]
        public int? rct_WeightUnitID { get; set; }

        [Column("rct_AgencyID")]
        public int? rct_AgencyID { get; set; }

        [Column("rct_TrailerID")]
        public int? rct_TrailerID { get; set; }

        [Column("rct_TruckID")]
        public int? rct_TruckID { get; set; }

        [Column("rct_DriverID")]
        public int? rct_DriverID { get; set; }

        [Column("rct_DocumentNumbers")]
        public string rct_DocumentNumbers { get; set; }

        [Column("rct_Memo")]
        public string rct_Memo { get; set; }

        [Column("rct_LogisticSiteID")]
        public int rct_LogisticSiteID { get; set; }

        [Column("rct_DomainID")]
        public int? rct_DomainID { get; set; }

        [Column("rct_ExportedLED")]
        public int? rct_ExportedLED { get; set; }

        [Column("rct_CargoReceiptID")]
        public int? rct_CargoReceiptID { get; set; }

        [Column("rct_AssignedEmployeeID")]
        public int? rct_AssignedEmployeeID { get; set; }

        [Column("rct_PlanCreatedLED")]
        public int? rct_PlanCreatedLED { get; set; }

        [Column("rct_WorkOrderID")]
        public int? rct_WorkOrderID { get; set; }

        [Column("rct_BilledLED")]
        public int? rct_BilledLED { get; set; }

        [Column("rct_LastUpdateTime")]
        public DateTime? rct_LastUpdateTime { get; set; }

        [Column("rct_LineCount")]
        public int? rct_LineCount { get; set; }

    }
}