namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_StockAttributes")]
    public class StockAttributes
    {
        [Column("sat_ID", true)] // Primary Key
        public int sat_ID { get; set; }

        [Column("sat_Code")]
        public string sat_Code { get; set; }

        [Column("sat_TypeID")]
        public int sat_TypeID { get; set; }

        [Column("sat_GroupID")]
        public int? sat_GroupID { get; set; }

        [Column("sat_DefaultValue")]
        public string sat_DefaultValue { get; set; }

        [Column("sat_DefaultCode")]
        public string sat_DefaultCode { get; set; }

        [Column("sat_IdentifierID")]
        public int? sat_IdentifierID { get; set; }

        [Column("sat_ProductAttributeID")]
        public int? sat_ProductAttributeID { get; set; }

        [Column("sat_Formula")]
        public string sat_Formula { get; set; }

        [Column("sat_Counter")]
        public int? sat_Counter { get; set; }

        [Column("sat_LastCounterRefValue")]
        public string sat_LastCounterRefValue { get; set; }

        [Column("sat_MultipleValuesLED")]
        public int? sat_MultipleValuesLED { get; set; }

        [Column("sat_UniquePerCULED")]
        public int? sat_UniquePerCULED { get; set; }

        [Column("sat_UseInVariantLED")]
        public int? sat_UseInVariantLED { get; set; }

        [Column("sat_UseInLotLED")]
        public int? sat_UseInLotLED { get; set; }

        [Column("sat_Comments")]
        public string sat_Comments { get; set; }

        [Column("sat_DomainID")]
        public int? sat_DomainID { get; set; }

        [Column("sat_DisplayOrder")]
        public int? sat_DisplayOrder { get; set; }

        [Column("sat_Transformation")]
        public string sat_Transformation { get; set; }

        [Column("sat_FillOnKeyPressLED")]
        public int? sat_FillOnKeyPressLED { get; set; }

        [Column("sat_MISIndex")]
        public int? sat_MISIndex { get; set; }

        [Column("sat_ValueListSQL")]
        public string sat_ValueListSQL { get; set; }

    }
}