namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_LocTransactionType")]
    public class LocTransactionType
    {
        [Column("ltr_ID", true)] // Primary Key
        public int ltr_ID { get; set; }

        [Column("ltr_LocationID")]
        public int? ltr_LocationID { get; set; }

        [Column("ltr_TransactionTypeID")]
        public int? ltr_TransactionTypeID { get; set; }

        [Column("ltr_SessionID")]
        public int? ltr_SessionID { get; set; }

        [Column("ltr_DomainID")]
        public int? ltr_DomainID { get; set; }

    }
}