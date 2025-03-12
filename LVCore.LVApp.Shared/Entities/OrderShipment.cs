namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_OrderShipment")]
    public class OrderShipment
    {
        [Column("ost_ID", true)] // Primary Key
        public int ost_ID { get; set; }

        [Column("ost_Code")]
        public string ost_Code { get; set; }

        [Column("ost_OrderID")]
        public int? ost_OrderID { get; set; }

        [Column("ost_GroupID")]
        public int? ost_GroupID { get; set; }

        [Column("ost_ShipmentID")]
        public int? ost_ShipmentID { get; set; }

        [Column("ost_ExecuteDate")]
        public DateTime? ost_ExecuteDate { get; set; }

        [Column("ost_ShipDate")]
        public DateTime? ost_ShipDate { get; set; }

        [Column("ost_DeliveryDate")]
        public DateTime? ost_DeliveryDate { get; set; }

        [Column("ost_Weight")]
        public decimal? ost_Weight { get; set; }

        [Column("ost_Volume")]
        public decimal? ost_Volume { get; set; }

        [Column("ost_WeightUnitID")]
        public int? ost_WeightUnitID { get; set; }

        [Column("ost_VolumeUnitID")]
        public int? ost_VolumeUnitID { get; set; }

        [Column("ost_RoutedLED")]
        public int? ost_RoutedLED { get; set; }

        [Column("ost_LoadingPriority")]
        public int? ost_LoadingPriority { get; set; }

        [Column("ost_DelayReasonID")]
        public int? ost_DelayReasonID { get; set; }

        [Column("ost_NoDeliveryLED")]
        public int? ost_NoDeliveryLED { get; set; }

        [Column("ost_NoDeliveryReasonID")]
        public int? ost_NoDeliveryReasonID { get; set; }

        [Column("ost_DispatchMethodID")]
        public int? ost_DispatchMethodID { get; set; }

        [Column("ost_PrintedLabelsLED")]
        public int? ost_PrintedLabelsLED { get; set; }

        [Column("ost_NonDeliveryActionID")]
        public int? ost_NonDeliveryActionID { get; set; }

        [Column("ost_StatusID")]
        public int? ost_StatusID { get; set; }

        [Column("ost_Timestamp")]
        public DateTime? ost_Timestamp { get; set; }

        [Column("ost_LogisticSiteID")]
        public int? ost_LogisticSiteID { get; set; }

        [Column("ost_DomainID")]
        public int? ost_DomainID { get; set; }

        [Column("ost_LockHostID")]
        public int? ost_LockHostID { get; set; }

        [Column("ost_AgencyID")]
        public int? ost_AgencyID { get; set; }

        [Column("ost_exportedLED")]
        public int? ost_exportedLED { get; set; }

        [Column("ost_AgencyZoneID")]
        public int? ost_AgencyZoneID { get; set; }

        [Column("ost_LastProcessDate")]
        public DateTime? ost_LastProcessDate { get; set; }

        [Column("ost_Memo")]
        public string ost_Memo { get; set; }

        [Column("ost_CompleteDate")]
        public DateTime? ost_CompleteDate { get; set; }

        [Column("ost_PlanDeliveryDate")]
        public DateTime? ost_PlanDeliveryDate { get; set; }

        [Column("ost_PlanDeliveryTimeFrom")]
        public DateTime? ost_PlanDeliveryTimeFrom { get; set; }

        [Column("ost_PlanDeliveryTimeTo")]
        public DateTime? ost_PlanDeliveryTimeTo { get; set; }

        [Column("ost_DeliveryTimeLimitFrom")]
        public DateTime? ost_DeliveryTimeLimitFrom { get; set; }

        [Column("ost_DeliveryTimeLimitTo")]
        public DateTime? ost_DeliveryTimeLimitTo { get; set; }

        [Column("ost_ConsignID")]
        public int? ost_ConsignID { get; set; }

        [Column("ost_OriginalConsignID")]
        public int? ost_OriginalConsignID { get; set; }

        [Column("ost_AgencyServiceID")]
        public int? ost_AgencyServiceID { get; set; }

        [Column("ost_OrderProfileID")]
        public int? ost_OrderProfileID { get; set; }

        [Column("ost_OrderBatchID")]
        public int? ost_OrderBatchID { get; set; }

    }
}