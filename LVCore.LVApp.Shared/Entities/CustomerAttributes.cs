namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_CustomerAttributes")]
    public class CustomerAttributes
    {
        [Column("cua_ID", true)] // Primary Key
        public int cua_ID { get; set; }

        [Column("cua_Code")]
        public string cua_Code { get; set; }

        [Column("cua_TypeID")]
        public int? cua_TypeID { get; set; }

        [Column("cua_DefaultValue")]
        public string cua_DefaultValue { get; set; }

        [Column("cua_Comments")]
        public string cua_Comments { get; set; }

        [Column("cua_DomainID")]
        public int? cua_DomainID { get; set; }

        [Column("cua_DisplayPriority")]
        public int? cua_DisplayPriority { get; set; }

        [Column("cua_ReadOnlyLED")]
        public int? cua_ReadOnlyLED { get; set; }

        [Column("cua_MISIndex")]
        public int? cua_MISIndex { get; set; }

    }
}