namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_TaskList")]
    public class TaskList
    {
        [Column("tkl_ID", true)] // Primary Key
        public int tkl_ID { get; set; }

        [Column("tkl_Code")]
        public string tkl_Code { get; set; }

        [Column("tkl_TransactionTypeID")]
        public int? tkl_TransactionTypeID { get; set; }

        [Column("tkl_CreateDate")]
        public DateTime? tkl_CreateDate { get; set; }

        [Column("tkl_CompleteDate")]
        public DateTime? tkl_CompleteDate { get; set; }

        [Column("tkl_CreateUserID")]
        public int? tkl_CreateUserID { get; set; }

        [Column("tkl_StatusID")]
        public int? tkl_StatusID { get; set; }

        [Column("tkl_LogisticSiteID")]
        public int tkl_LogisticSiteID { get; set; }

        [Column("tkl_PreviousListID")]
        public int? tkl_PreviousListID { get; set; }

        [Column("tkl_OrderTypeID")]
        public int? tkl_OrderTypeID { get; set; }

        [Column("tkl_ReasonCode")]
        public string tkl_ReasonCode { get; set; }

        [Column("tkl_DomainID")]
        public int? tkl_DomainID { get; set; }

        [Column("tkl_OriginalReasonCode")]
        public string tkl_OriginalReasonCode { get; set; }

        [Column("tkl_Comments")]
        public string tkl_Comments { get; set; }

    }
}