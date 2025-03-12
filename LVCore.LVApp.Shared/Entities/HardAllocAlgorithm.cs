namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_HardAllocAlgorithm")]
    public class HardAllocAlgorithm
    {
        [Column("haa_ID", true)] // Primary Key
        public int haa_ID { get; set; }

        [Column("haa_Code")]
        public string haa_Code { get; set; }

        [Column("haa_Description")]
        public string haa_Description { get; set; }

        [Column("haa_DomainID")]
        public int? haa_DomainID { get; set; }

        [Column("haa_Algorithm")]
        public object haa_Algorithm { get; set; }

        [Column("haa_Compiled")]
        public object haa_Compiled { get; set; }

    }
}