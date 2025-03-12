namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_MSRByDimension")]
    public class MSRByDimension
    {
        [Column("msb_ID", true)] // Primary Key
        public int msb_ID { get; set; }

        [Column("msb_Code")]
        public string msb_Code { get; set; }

        [Column("msb_TableName")]
        public string msb_TableName { get; set; }

        [Column("msb_DimensionGroupID")]
        public int? msb_DimensionGroupID { get; set; }

        [Column("msb_DepositorID")]
        public int? msb_DepositorID { get; set; }

        [Column("msb_LogisticSiteID")]
        public int? msb_LogisticSiteID { get; set; }

        [Column("msb_LogisticUnitID")]
        public int? msb_LogisticUnitID { get; set; }

        [Column("msb_Dimension")]
        public int? msb_Dimension { get; set; }

        [Column("msb_DomainID")]
        public int? msb_DomainID { get; set; }

    }
}