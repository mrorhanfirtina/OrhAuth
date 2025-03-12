namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_EmployeesByGroup")]
    public class EmployeesByGroup
    {
        [Column("ebg_ID", true)] // Primary Key
        public int ebg_ID { get; set; }

        [Column("ebg_GroupID")]
        public int? ebg_GroupID { get; set; }

        [Column("ebg_EmployeeID")]
        public int? ebg_EmployeeID { get; set; }

        [Column("ebg_DomainID")]
        public int? ebg_DomainID { get; set; }

    }
}