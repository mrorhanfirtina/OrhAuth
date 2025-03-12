namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_SupplierAttributes")]
    public class SupplierAttributes
    {
        [Column("sua_ID", true)] // Primary Key
        public int sua_ID { get; set; }

        [Column("sua_Code")]
        public string sua_Code { get; set; }

        [Column("sua_TypeID")]
        public int? sua_TypeID { get; set; }

        [Column("sua_DefaultValue")]
        public string sua_DefaultValue { get; set; }

        [Column("sua_Comments")]
        public string sua_Comments { get; set; }

        [Column("sua_DisplayPriority")]
        public int? sua_DisplayPriority { get; set; }

        [Column("sua_ReadOnlyLED")]
        public int? sua_ReadOnlyLED { get; set; }

        [Column("sua_DomainID")]
        public int? sua_DomainID { get; set; }

        [Column("sua_MISIndex")]
        public int? sua_MISIndex { get; set; }

    }
}