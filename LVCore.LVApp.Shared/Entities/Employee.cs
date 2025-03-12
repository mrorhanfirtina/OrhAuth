namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("COM_Employee")]
    public class Employee
    {
        [Column("emp_ID", true)] // Primary Key
        public int emp_ID { get; set; }

        [Column("emp_PersonID")]
        public int? emp_PersonID { get; set; }

        [Column("emp_HireDate")]
        public DateTime? emp_HireDate { get; set; }

        [Column("emp_ActiveLED")]
        public int? emp_ActiveLED { get; set; }

        [Column("emp_DepartmentID")]
        public int? emp_DepartmentID { get; set; }

        [Column("emp_LogisticUnitID")]
        public int? emp_LogisticUnitID { get; set; }

        [Column("emp_WorkTimeModelID")]
        public int? emp_WorkTimeModelID { get; set; }

        [Column("emp_Card")]
        public string emp_Card { get; set; }

        [Column("emp_Salary")]
        public decimal? emp_Salary { get; set; }

        [Column("emp_WorkHoursPerWeek")]
        public decimal? emp_WorkHoursPerWeek { get; set; }

        [Column("emp_HourlyRate")]
        public decimal? emp_HourlyRate { get; set; }

        [Column("emp_DomainID")]
        public int? emp_DomainID { get; set; }

    }
}