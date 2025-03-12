namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_OperationsByGroup")]
    public class OperationsByGroup
    {
        [Column("obg_ID", true)] // Primary Key
        public int obg_ID { get; set; }

        [Column("obg_OperationID")]
        public int? obg_OperationID { get; set; }

        [Column("obg_GroupID")]
        public int? obg_GroupID { get; set; }

        [Column("obg_DomainID")]
        public int? obg_DomainID { get; set; }

    }
}