namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ProductDepositor")]
    public class ProductDepositor
    {
        [Column("pdp_ID", true)] // Primary Key
        public int pdp_ID { get; set; }

        [Column("pdp_ProductID")]
        public int? pdp_ProductID { get; set; }

        [Column("pdp_DepositorID")]
        public int? pdp_DepositorID { get; set; }

        [Column("pdp_DomainID")]
        public int? pdp_DomainID { get; set; }

    }
}