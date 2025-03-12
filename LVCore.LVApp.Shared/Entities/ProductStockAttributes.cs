namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ProductStockAttributes")]
    public class ProductStockAttributes
    {
        [Column("psa_ID", true)] // Primary Key
        public int psa_ID { get; set; }

        [Column("psa_ProductID")]
        public int? psa_ProductID { get; set; }

        [Column("psa_AttributeID")]
        public int? psa_AttributeID { get; set; }

        [Column("psa_MultipleValuesLED")]
        public int? psa_MultipleValuesLED { get; set; }

        [Column("psa_UniquePerCULED")]
        public int? psa_UniquePerCULED { get; set; }

        [Column("psa_AutoValueOverrideID")]
        public int? psa_AutoValueOverrideID { get; set; }

        [Column("psa_UseInVariantLED")]
        public int? psa_UseInVariantLED { get; set; }

        [Column("psa_UseInLotLED")]
        public int? psa_UseInLotLED { get; set; }

        [Column("psa_OutboundPolicyID")]
        public int? psa_OutboundPolicyID { get; set; }

        [Column("psa_PolicyPriority")]
        public int? psa_PolicyPriority { get; set; }

        [Column("psa_UseInInvLED")]
        public int? psa_UseInInvLED { get; set; }

        [Column("psa_UseInInvExtLED")]
        public int? psa_UseInInvExtLED { get; set; }

        [Column("psa_Counter")]
        public int? psa_Counter { get; set; }

        [Column("psa_DomainID")]
        public int? psa_DomainID { get; set; }

        [Column("psa_OrderPreferenceLED")]
        public int? psa_OrderPreferenceLED { get; set; }

        [Column("psa_DriverLED")]
        public int? psa_DriverLED { get; set; }

        [Column("psa_SeriesLED")]
        public int? psa_SeriesLED { get; set; }

    }
}