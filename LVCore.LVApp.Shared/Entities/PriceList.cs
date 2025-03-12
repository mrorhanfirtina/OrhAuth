namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_PriceList")]
    public class PriceList
    {
        [Column("pcl_ID", true)] // Primary Key
        public int pcl_ID { get; set; }

        [Column("pcl_Code")]
        public string pcl_Code { get; set; }

        [Column("pcl_PriceListTypeID")]
        public int? pcl_PriceListTypeID { get; set; }

        [Column("pcl_BIRangeChargeTypeID")]
        public int? pcl_BIRangeChargeTypeID { get; set; }

        [Column("pcl_PriceListObjID")]
        public int? pcl_PriceListObjID { get; set; }

        [Column("pcl_MinimumCharge")]
        public decimal? pcl_MinimumCharge { get; set; }

        [Column("pcl_DomainID")]
        public int? pcl_DomainID { get; set; }

        [Column("pcl_MaximumCharge")]
        public decimal? pcl_MaximumCharge { get; set; }

        [Column("pcl_CreateUserID")]
        public int? pcl_CreateUserID { get; set; }

        [Column("pcl_LastUpdateUserID")]
        public int? pcl_LastUpdateUserID { get; set; }

    }
}