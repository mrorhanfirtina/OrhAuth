namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Unit")]
    public class Unit
    {
        [Column("unt_ID", true)] // Primary Key
        public int unt_ID { get; set; }

        [Column("unt_Code")]
        public string unt_Code { get; set; }

        [Column("unt_TypeID")]
        public int? unt_TypeID { get; set; }

        [Column("unt_DimensionID")]
        public int? unt_DimensionID { get; set; }

        [Column("unt_DefaultLED")]
        public int? unt_DefaultLED { get; set; }

        [Column("unt_Conversion")]
        public decimal? unt_Conversion { get; set; }

        [Column("unt_ConvExponent")]
        public int? unt_ConvExponent { get; set; }

        [Column("unt_Rounding")]
        public int? unt_Rounding { get; set; }

        [Column("unt_DomainID")]
        public int? unt_DomainID { get; set; }

        [Column("unt_VariableDeadWeightLED")]
        public int? unt_VariableDeadWeightLED { get; set; }

    }
}