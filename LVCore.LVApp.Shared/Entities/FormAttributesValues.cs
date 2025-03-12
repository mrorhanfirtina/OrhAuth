namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_FormAttributesValues")]
    public class FormAttributesValues
    {
        [Column("fav_ID", true)] // Primary Key
        public int fav_ID { get; set; }

        [Column("fav_RecordID")]
        public int? fav_RecordID { get; set; }

        [Column("fav_FormID")]
        public int? fav_FormID { get; set; }

        [Column("fav_AttributeID")]
        public int? fav_AttributeID { get; set; }

        [Column("fav_Value")]
        public string fav_Value { get; set; }

        [Column("fav_DomainID")]
        public int? fav_DomainID { get; set; }

    }
}