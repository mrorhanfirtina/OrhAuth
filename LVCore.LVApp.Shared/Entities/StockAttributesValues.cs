namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_StockAttributesValues")]
    public class StockAttributesValues
    {
        [Column("sav_ID", true)] // Primary Key
        public int sav_ID { get; set; }

        [Column("sav_StockID")]
        public int? sav_StockID { get; set; }

        [Column("sav_attributeID")]
        public int? sav_attributeID { get; set; }

        [Column("sav_Value")]
        public string sav_Value { get; set; }

        [Column("sav_Timestamp")]
        public DateTime? sav_Timestamp { get; set; }

        [Column("sav_DomainID")]
        public int? sav_DomainID { get; set; }

    }
}