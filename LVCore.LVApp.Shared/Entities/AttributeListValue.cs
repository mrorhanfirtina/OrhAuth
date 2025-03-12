namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_AttributeListValue")]
    public class AttributeListValue
    {
        [Column("all_ID", true)] // Primary Key
        public int all_ID { get; set; }

        [Column("all_RecAttrListID")]
        public int? all_RecAttrListID { get; set; }

        [Column("all_OrdAttrListID")]
        public int? all_OrdAttrListID { get; set; }

        [Column("all_PrdAttrListID")]
        public int? all_PrdAttrListID { get; set; }

        [Column("all_StkAttrListID")]
        public int? all_StkAttrListID { get; set; }

        [Column("all_Value")]
        public string all_Value { get; set; }

        [Column("all_LanguageID")]
        public int all_LanguageID { get; set; }

        [Column("all_DomainID")]
        public int all_DomainID { get; set; }

        [Column("all_CharacteristicListID")]
        public int? all_CharacteristicListID { get; set; }

        [Column("all_CustAttrListID")]
        public int? all_CustAttrListID { get; set; }

        [Column("all_SplAttrListID")]
        public int? all_SplAttrListID { get; set; }

        [Column("all_ShpAttrListID")]
        public int? all_ShpAttrListID { get; set; }

        [Column("all_DepAttrListID")]
        public int? all_DepAttrListID { get; set; }

        [Column("all_FrmAttrListID")]
        public int? all_FrmAttrListID { get; set; }

    }
}