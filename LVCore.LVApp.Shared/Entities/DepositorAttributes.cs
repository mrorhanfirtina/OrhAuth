namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_DepositorAttributes")]
    public class DepositorAttributes
    {
        [Column("dea_ID", true)] // Primary Key
        public int dea_ID { get; set; }

        [Column("dea_Code")]
        public string dea_Code { get; set; }

        [Column("dea_TypeID")]
        public int? dea_TypeID { get; set; }

        [Column("dea_DefaultValue")]
        public string dea_DefaultValue { get; set; }

        [Column("dea_Comments")]
        public string dea_Comments { get; set; }

        [Column("dea_DisplayPriority")]
        public int? dea_DisplayPriority { get; set; }

        [Column("dea_MISIndex")]
        public int? dea_MISIndex { get; set; }

        [Column("dea_DomainID")]
        public int? dea_DomainID { get; set; }

    }
}