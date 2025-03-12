namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_Datasource")]
    public class Datasource
    {
        [Column("dsc_ID", true)] // Primary Key
        public int dsc_ID { get; set; }

        [Column("dsc_Code")]
        public string dsc_Code { get; set; }

        [Column("dsc_Description")]
        public string dsc_Description { get; set; }

        [Column("dsc_SQLCommand")]
        public string dsc_SQLCommand { get; set; }

        [Column("dsc_DomainID")]
        public int? dsc_DomainID { get; set; }

    }
}