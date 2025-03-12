namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_STO_ResourceDataByLU")]
    public class STOResourceDataByLU
    {
        [Column("ust_ID", true)] // Primary Key
        public int ust_ID { get; set; }

        [Column("ust_LogisticUnitID")]
        public int? ust_LogisticUnitID { get; set; }

        [Column("ust_StorageResourceTypeID")]
        public int? ust_StorageResourceTypeID { get; set; }

        [Column("ust_WMSControlledLED")]
        public int? ust_WMSControlledLED { get; set; }

        [Column("ust_ProductionReceiptLocID")]
        public int? ust_ProductionReceiptLocID { get; set; }

        [Column("ust_DirectReceiptLED")]
        public int? ust_DirectReceiptLED { get; set; }

        [Column("ust_DirectIssueLED")]
        public int? ust_DirectIssueLED { get; set; }

        [Column("ust_DomainID")]
        public int? ust_DomainID { get; set; }

    }
}