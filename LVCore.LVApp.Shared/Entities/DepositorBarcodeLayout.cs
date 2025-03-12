namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorBarcodeLayout")]
    public class DepositorBarcodeLayout
    {
        [Column("dbl_ID", true)] // Primary Key
        public int dbl_ID { get; set; }

        [Column("dbl_DepositorID")]
        public int? dbl_DepositorID { get; set; }

        [Column("dbl_BarcodeLayoutID")]
        public int? dbl_BarcodeLayoutID { get; set; }

        [Column("dbl_DomainID")]
        public int? dbl_DomainID { get; set; }

    }
}