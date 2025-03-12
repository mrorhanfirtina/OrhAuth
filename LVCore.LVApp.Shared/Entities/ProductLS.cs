namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_ProductLS")]
    public class ProductLS
    {
        [Column("pls_ID", true)] // Primary Key
        public int pls_ID { get; set; }

        [Column("pls_ProductID")]
        public int? pls_ProductID { get; set; }

        [Column("pls_SafetyStock")]
        public decimal? pls_SafetyStock { get; set; }

        [Column("pls_SNLED")]
        public int? pls_SNLED { get; set; }

        [Column("pls_RetentionPercentage")]
        public decimal? pls_RetentionPercentage { get; set; }

        [Column("pls_SUValue")]
        public decimal? pls_SUValue { get; set; }

        [Column("pls_LogisticSiteID")]
        public int? pls_LogisticSiteID { get; set; }

        [Column("pls_DefaultLUID")]
        public int? pls_DefaultLUID { get; set; }

        [Column("pls_DomainID")]
        public int? pls_DomainID { get; set; }

        [Column("pls_CountPeriod")]
        public int? pls_CountPeriod { get; set; }

        [Column("pls_CountDate")]
        public DateTime? pls_CountDate { get; set; }

        [Column("pls_ReorderReviewPeriod")]
        public int? pls_ReorderReviewPeriod { get; set; }

        [Column("pls_POLeadTime")]
        public int? pls_POLeadTime { get; set; }

        [Column("pls_TopQtySNLED")]
        public int? pls_TopQtySNLED { get; set; }

        [Column("pls_MinReplQty")]
        public decimal? pls_MinReplQty { get; set; }

        [Column("pls_ReplItemUnitID")]
        public int? pls_ReplItemUnitID { get; set; }

        [Column("pls_PrimCustItemUnitID")]
        public int? pls_PrimCustItemUnitID { get; set; }

        [Column("pls_CustValueItemUnitID")]
        public int? pls_CustValueItemUnitID { get; set; }

        [Column("pls_CustomsUnitPrice")]
        public decimal? pls_CustomsUnitPrice { get; set; }

        [Column("pls_SNPerSSCCLED")]
        public int? pls_SNPerSSCCLED { get; set; }

    }
}