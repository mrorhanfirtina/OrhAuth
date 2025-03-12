namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_FormAttributes")]
    public class FormAttributes
    {
        [Column("fat_ID", true)] // Primary Key
        public int fat_ID { get; set; }

        [Column("fat_Code")]
        public string fat_Code { get; set; }

        [Column("fat_TypeID")]
        public int? fat_TypeID { get; set; }

        [Column("fat_DefaultValue")]
        public string fat_DefaultValue { get; set; }

        [Column("fat_Comments")]
        public string fat_Comments { get; set; }

        [Column("fat_DisplayPriority")]
        public int? fat_DisplayPriority { get; set; }

        [Column("fat_ReadOnlyLED")]
        public int? fat_ReadOnlyLED { get; set; }

        [Column("fat_RequiredLED")]
        public int? fat_RequiredLED { get; set; }

        [Column("fat_DomainID")]
        public int? fat_DomainID { get; set; }

    }
}