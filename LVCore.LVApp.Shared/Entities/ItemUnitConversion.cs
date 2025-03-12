namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ItemUnitConversion")]
    public class ItemUnitConversion
    {
        [Column("iuc_ID", true)] // Primary Key
        public int iuc_ID { get; set; }

        [Column("iuc_ProductID")]
        public int? iuc_ProductID { get; set; }

        [Column("iuc_ConvertedUnitID")]
        public int? iuc_ConvertedUnitID { get; set; }

        [Column("iuc_ReferenceUnitID")]
        public int? iuc_ReferenceUnitID { get; set; }

        [Column("iuc_Conversion")]
        public decimal? iuc_Conversion { get; set; }

        [Column("iuc_ConvExponent")]
        public int? iuc_ConvExponent { get; set; }

        [Column("iuc_InvConversion")]
        public decimal? iuc_InvConversion { get; set; }

        [Column("iuc_InvConvExponent")]
        public int? iuc_InvConvExponent { get; set; }

        [Column("iuc_DomainID")]
        public int? iuc_DomainID { get; set; }

    }
}