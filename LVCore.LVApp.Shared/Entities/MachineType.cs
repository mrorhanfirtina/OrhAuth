namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_MachineType")]
    public class MachineType
    {
        [Column("mnt_ID", true)] // Primary Key
        public int mnt_ID { get; set; }

        [Column("mnt_Code")]
        public string mnt_Code { get; set; }

        [Column("mnt_Description")]
        public string mnt_Description { get; set; }

        [Column("mnt_DomainID")]
        public int? mnt_DomainID { get; set; }

    }
}