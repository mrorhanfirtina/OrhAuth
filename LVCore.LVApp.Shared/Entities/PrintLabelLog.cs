namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_PrintLabelLog")]
    public class PrintLabelLog
    {
        [Column("pll_ID", true)] // Primary Key
        public int pll_ID { get; set; }

        [Column("pll_Datetime")]
        public DateTime? pll_Datetime { get; set; }

        [Column("pll_UserID")]
        public int? pll_UserID { get; set; }

        [Column("pll_PrintLabelID")]
        public int? pll_PrintLabelID { get; set; }

        [Column("pll_DepositorID")]
        public int? pll_DepositorID { get; set; }

        [Column("pll_ProductID")]
        public int? pll_ProductID { get; set; }

        [Column("pll_Quantity")]
        public int? pll_Quantity { get; set; }

        [Column("pll_LogisticSiteID")]
        public int? pll_LogisticSiteID { get; set; }

        [Column("pll_DomainID")]
        public int? pll_DomainID { get; set; }

        [Column("pll_OriginCode")]
        public string pll_OriginCode { get; set; }

    }
}