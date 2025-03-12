namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_MSRByDimElements")]
    public class MSRByDimElements
    {
        [Column("mse_ID", true)] // Primary Key
        public int mse_ID { get; set; }

        [Column("mse_DimensionID")]
        public int? mse_DimensionID { get; set; }

        [Column("mse_AutoNumber")]
        public int? mse_AutoNumber { get; set; }

        [Column("mse_ElementTypeID")]
        public int? mse_ElementTypeID { get; set; }

        [Column("mse_MeasurementID")]
        public int? mse_MeasurementID { get; set; }

        [Column("Mse_Description")]
        public string Mse_Description { get; set; }

        [Column("mse_AggregateTypeID")]
        public int? mse_AggregateTypeID { get; set; }

        [Column("mse_Formula")]
        public string mse_Formula { get; set; }

        [Column("mse_PriceListID")]
        public int? mse_PriceListID { get; set; }

        [Column("mse_Price")]
        public decimal? mse_Price { get; set; }

        [Column("mse_DomainID")]
        public int? mse_DomainID { get; set; }

        [Column("mse_ChargeItemID")]
        public int? mse_ChargeItemID { get; set; }

    }
}