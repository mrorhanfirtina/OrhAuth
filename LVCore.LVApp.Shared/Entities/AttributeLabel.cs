namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_AttributeLabel")]
    public class AttributeLabel
    {
        [Column("atl_ID", true)] // Primary Key
        public int atl_ID { get; set; }

        [Column("atl_RecAttrID")]
        public int? atl_RecAttrID { get; set; }

        [Column("atl_OrdAttrID")]
        public int? atl_OrdAttrID { get; set; }

        [Column("atl_PrdAttrID")]
        public int? atl_PrdAttrID { get; set; }

        [Column("atl_StkAttrID")]
        public int? atl_StkAttrID { get; set; }

        [Column("atl_DynAttrID")]
        public int? atl_DynAttrID { get; set; }

        [Column("atl_Label")]
        public string atl_Label { get; set; }

        [Column("atl_RFLabel")]
        public string atl_RFLabel { get; set; }

        [Column("atl_LanguageID")]
        public int atl_LanguageID { get; set; }

        [Column("atl_DomainID")]
        public int atl_DomainID { get; set; }

        [Column("atl_CharacteristicID")]
        public int? atl_CharacteristicID { get; set; }

        [Column("atl_CustAttrId")]
        public int? atl_CustAttrId { get; set; }

        [Column("atl_SplAttrID")]
        public int? atl_SplAttrID { get; set; }

        [Column("atl_ShpAttrID")]
        public int? atl_ShpAttrID { get; set; }

        [Column("atl_DepAttrID")]
        public int? atl_DepAttrID { get; set; }

        [Column("atl_FrmAttrID")]
        public int? atl_FrmAttrID { get; set; }

    }
}