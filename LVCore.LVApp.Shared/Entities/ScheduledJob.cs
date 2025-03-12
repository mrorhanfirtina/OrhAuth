namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("COM_ScheduledJob")]
    public class ScheduledJob
    {
        [Column("sjm_ID", true)] // Primary Key
        public int sjm_ID { get; set; }

        [Column("sjm_Code")]
        public string sjm_Code { get; set; }

        [Column("sjm_Description")]
        public string sjm_Description { get; set; }

        [Column("sjm_ActiveLed")]
        public int? sjm_ActiveLed { get; set; }

        [Column("sjm_ScheduledJobTypeID")]
        public int? sjm_ScheduledJobTypeID { get; set; }

        [Column("sjm_ScheduleTypeID")]
        public int? sjm_ScheduleTypeID { get; set; }

        [Column("sjm_StartDate")]
        public DateTime? sjm_StartDate { get; set; }

        [Column("sjm_EndDate")]
        public DateTime? sjm_EndDate { get; set; }

        [Column("sjm_RepeatPeriodTypeID")]
        public int? sjm_RepeatPeriodTypeID { get; set; }

        [Column("sjm_RepeatPeriod")]
        public string sjm_RepeatPeriod { get; set; }

        [Column("sjm_StartTime")]
        public DateTime? sjm_StartTime { get; set; }

        [Column("sjm_LastRunDate")]
        public DateTime? sjm_LastRunDate { get; set; }

        [Column("sjm_NextRunDate")]
        public DateTime? sjm_NextRunDate { get; set; }

        [Column("sjm_EmailOnSuccess")]
        public string sjm_EmailOnSuccess { get; set; }

        [Column("sjm_EmailOnFailure")]
        public string sjm_EmailOnFailure { get; set; }

        [Column("sjm_DomainID")]
        public int? sjm_DomainID { get; set; }

        [Column("sjm_ParentJobID")]
        public int? sjm_ParentJobID { get; set; }

        [Column("sjm_ProcessLed")]
        public int? sjm_ProcessLed { get; set; }

        [Column("sjm_ExportID")]
        public int? sjm_ExportID { get; set; }

        [Column("sjm_ServiceInstance")]
        public string sjm_ServiceInstance { get; set; }

        [Column("sjm_LogSuccessLED")]
        public int? sjm_LogSuccessLED { get; set; }

        [Column("sjm_SchedulerIP")]
        public string sjm_SchedulerIP { get; set; }

        [Column("sjm_SchedulerPort")]
        public string sjm_SchedulerPort { get; set; }

        [Column("sjm_UseWeeklyScheduleLED")]
        public int? sjm_UseWeeklyScheduleLED { get; set; }

        [Column("sjm_LogToTableLED")]
        public int? sjm_LogToTableLED { get; set; }

        [Column("sjm_SchedulerURL")]
        public string sjm_SchedulerURL { get; set; }

    }
}