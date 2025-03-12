namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_StorageSystems")]
    public class StorageSystems
    {
        [Column("sts_ID", true)] // Primary Key
        public int sts_ID { get; set; }

        [Column("sts_Code")]
        public string sts_Code { get; set; }

        [Column("sts_Description")]
        public string sts_Description { get; set; }

        [Column("sts_SingleProductLed")]
        public int? sts_SingleProductLed { get; set; }

        [Column("sts_CapacityMeasurementID")]
        public int? sts_CapacityMeasurementID { get; set; }

        [Column("sts_DomainID")]
        public int? sts_DomainID { get; set; }

        [Column("sts_HasLevelsLed")]
        public int? sts_HasLevelsLed { get; set; }

        [Column("sts_HasColumnsLed")]
        public int? sts_HasColumnsLed { get; set; }

        [Column("sts_SinglePackTypeLed")]
        public int? sts_SinglePackTypeLed { get; set; }

        [Column("sts_MonitorSequenceLED")]
        public int? sts_MonitorSequenceLED { get; set; }

        [Column("sts_MixFluidsLotLED")]
        public int? sts_MixFluidsLotLED { get; set; }

        [Column("sts_LifoFifoCode")]
        public int? sts_LifoFifoCode { get; set; }

        [Column("sts_FillPercentage")]
        public int? sts_FillPercentage { get; set; }

        [Column("sts_LocCodeFormat")]
        public string sts_LocCodeFormat { get; set; }

        [Column("sts_CustomAssemblyName")]
        public string sts_CustomAssemblyName { get; set; }

        [Column("sts_CustomTypeName")]
        public string sts_CustomTypeName { get; set; }

    }
}