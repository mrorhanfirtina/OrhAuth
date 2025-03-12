namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_LogisticUnit")]
    public class LogisticUnit
    {
        [Column("lou_ID", true)] // Primary Key
        public int lou_ID { get; set; }

        [Column("lou_Code")]
        public string lou_Code { get; set; }

        [Column("lou_Name")]
        public string lou_Name { get; set; }

        [Column("lou_LinkStorageResourcesLED")]
        public int? lou_LinkStorageResourcesLED { get; set; }

        [Column("lou_LinkHumanResourcesLED")]
        public int? lou_LinkHumanResourcesLED { get; set; }

        [Column("lou_LinkTechnicalResourcesLED")]
        public int? lou_LinkTechnicalResourcesLED { get; set; }

        [Column("lou_LogisticSiteID")]
        public int? lou_LogisticSiteID { get; set; }

        [Column("lou_DomainID")]
        public int? lou_DomainID { get; set; }

    }
}