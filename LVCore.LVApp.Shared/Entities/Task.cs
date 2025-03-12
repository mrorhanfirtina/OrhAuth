namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Task")]
    public class Task
    {
        [Column("tsk_ID", true)] // Primary Key
        public int tsk_ID { get; set; }

        [Column("tsk_Code")]
        public string tsk_Code { get; set; }

        [Column("tsk_TaskListID")]
        public int? tsk_TaskListID { get; set; }

        [Column("tsk_CreateTime")]
        public DateTime? tsk_CreateTime { get; set; }

        [Column("tsk_CreateUserID")]
        public int? tsk_CreateUserID { get; set; }

        [Column("tsk_ExpUserID")]
        public int? tsk_ExpUserID { get; set; }

        [Column("tsk_ActualUserID")]
        public int? tsk_ActualUserID { get; set; }

        [Column("tsk_ActualTime")]
        public DateTime? tsk_ActualTime { get; set; }

        [Column("tsk_ActualSessionID")]
        public int? tsk_ActualSessionID { get; set; }

        [Column("tsk_FromLocationID")]
        public int? tsk_FromLocationID { get; set; }

        [Column("tsk_FromLocationCode")]
        public string tsk_FromLocationCode { get; set; }

        [Column("tsk_ToLocationID")]
        public int? tsk_ToLocationID { get; set; }

        [Column("tsk_ToLocationCode")]
        public string tsk_ToLocationCode { get; set; }

        [Column("tsk_ToContainerID")]
        public int? tsk_ToContainerID { get; set; }

        [Column("tsk_TransactionTypeID")]
        public int? tsk_TransactionTypeID { get; set; }

        [Column("tsk_StatusID")]
        public int? tsk_StatusID { get; set; }

        [Column("tsk_ContainerID")]
        public int? tsk_ContainerID { get; set; }

        [Column("tsk_ContUnitID")]
        public int? tsk_ContUnitID { get; set; }

        [Column("tsk_ExpContainerID")]
        public int? tsk_ExpContainerID { get; set; }

        [Column("tsk_SSCC")]
        public string tsk_SSCC { get; set; }

        [Column("tsk_DepositorID")]
        public int? tsk_DepositorID { get; set; }

        [Column("tsk_ProductID")]
        public int? tsk_ProductID { get; set; }

        [Column("tsk_UnsuitReasonID")]
        public int? tsk_UnsuitReasonID { get; set; }

        [Column("tsk_ReserveReasonID")]
        public int? tsk_ReserveReasonID { get; set; }

        [Column("tsk_ItemUnitID")]
        public int? tsk_ItemUnitID { get; set; }

        [Column("tsk_Quantity")]
        public decimal? tsk_Quantity { get; set; }

        [Column("tsk_Timestamp")]
        public DateTime? tsk_Timestamp { get; set; }

        [Column("tsk_PriorityID")]
        public int? tsk_PriorityID { get; set; }

        [Column("tsk_ExecutionOrder")]
        public int? tsk_ExecutionOrder { get; set; }

        [Column("tsk_LogisticSiteID")]
        public int tsk_LogisticSiteID { get; set; }

        [Column("tsk_ReceiptItemID")]
        public int? tsk_ReceiptItemID { get; set; }

        [Column("tsk_DomainID")]
        public int? tsk_DomainID { get; set; }

        [Column("tsk_SuspRsnDependLED")]
        public int? tsk_SuspRsnDependLED { get; set; }

        [Column("tsk_SuspRsnSpaceLED")]
        public int? tsk_SuspRsnSpaceLED { get; set; }

        [Column("tsk_FinalLocationID")]
        public int? tsk_FinalLocationID { get; set; }

        [Column("tsk_FinalLocationCode")]
        public string tsk_FinalLocationCode { get; set; }

        [Column("tsk_AssignTime")]
        public DateTime? tsk_AssignTime { get; set; }

        [Column("tsk_ExportedLED")]
        public int? tsk_ExportedLED { get; set; }

    }
}