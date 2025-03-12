namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_RsrvRsnForbidTransType")]
    public class RsrvRsnForbidTransType
    {
        [Column("rrt_ID", true)] // Primary Key
        public int rrt_ID { get; set; }

        [Column("rrt_ReserveReasonID")]
        public int? rrt_ReserveReasonID { get; set; }

        [Column("rrt_TransactionTypeID")]
        public int? rrt_TransactionTypeID { get; set; }

        [Column("rrt_DomainID")]
        public int? rrt_DomainID { get; set; }

    }
}