namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Customer")]
    public class Customer
    {
        [Column("cus_ID", true)] // Primary Key
        public int cus_ID { get; set; }

        [Column("cus_Code")]
        public string cus_Code { get; set; }

        [Column("cus_CompanyID")]
        public int? cus_CompanyID { get; set; }

        [Column("cus_DepositorID")]
        public int? cus_DepositorID { get; set; }

        [Column("cus_CustomerCategoryID")]
        public int? cus_CustomerCategoryID { get; set; }

        [Column("cus_DeliveryDuration")]
        public int? cus_DeliveryDuration { get; set; }

        [Column("cus_BatchShipLED")]
        public int? cus_BatchShipLED { get; set; }

        [Column("cus_FlexibilityIndicator")]
        public int? cus_FlexibilityIndicator { get; set; }

        [Column("cus_ABCCategoryID")]
        public int? cus_ABCCategoryID { get; set; }

        [Column("cus_RampLED")]
        public int? cus_RampLED { get; set; }

        [Column("cus_MinTruckFloorHeight")]
        public int? cus_MinTruckFloorHeight { get; set; }

        [Column("cus_MaxTruckFloorHeight")]
        public int? cus_MaxTruckFloorHeight { get; set; }

        [Column("cus_HeightUnitID")]
        public int? cus_HeightUnitID { get; set; }

        [Column("cus_DomainID")]
        public int? cus_DomainID { get; set; }

        [Column("cus_MasterCustomerID")]
        public int? cus_MasterCustomerID { get; set; }

        [Column("cus_LastUpdateTime")]
        public DateTime? cus_LastUpdateTime { get; set; }

    }
}