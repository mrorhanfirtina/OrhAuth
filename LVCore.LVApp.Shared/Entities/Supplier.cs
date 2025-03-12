namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Supplier")]
    public class Supplier
    {
        [Column("spl_ID", true)] // Primary Key
        public int spl_ID { get; set; }

        [Column("spl_Code")]
        public string spl_Code { get; set; }

        [Column("spl_CompanyID")]
        public int? spl_CompanyID { get; set; }

        [Column("spl_LayoutWithAILED")]
        public int? spl_LayoutWithAILED { get; set; }

        [Column("spl_LayoutLines")]
        public int? spl_LayoutLines { get; set; }

        [Column("spl_DomainID")]
        public int? spl_DomainID { get; set; }

        [Column("spl_UseCommonLayoutLED")]
        public int? spl_UseCommonLayoutLED { get; set; }

        [Column("spl_LastUpdateTime")]
        public DateTime? spl_LastUpdateTime { get; set; }

    }
}