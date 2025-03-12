namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_MachineModel")]
    public class MachineModel
    {
        [Column("mnm_ID", true)] // Primary Key
        public int mnm_ID { get; set; }

        [Column("mnm_Code")]
        public string mnm_Code { get; set; }

        [Column("mnm_Description")]
        public string mnm_Description { get; set; }

        [Column("mnm_TypeID")]
        public int? mnm_TypeID { get; set; }

        [Column("mnm_DomainID")]
        public int? mnm_DomainID { get; set; }

    }
}