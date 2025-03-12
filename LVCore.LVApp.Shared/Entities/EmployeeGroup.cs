namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_EmployeeGroup")]
    public class EmployeeGroup
    {
        [Column("egr_ID", true)] // Primary Key
        public int egr_ID { get; set; }

        [Column("egr_Code")]
        public string egr_Code { get; set; }

        [Column("egr_Name")]
        public string egr_Name { get; set; }

        [Column("egr_LogisticSiteID")]
        public int? egr_LogisticSiteID { get; set; }

        [Column("egr_DomainID")]
        public int? egr_DomainID { get; set; }

    }
}