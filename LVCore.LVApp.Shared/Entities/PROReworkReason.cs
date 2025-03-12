namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("PRO_ReworkReason")]
    public class PROReworkReason
    {
        [Column("rwk_ID", true)] // Primary Key
        public int rwk_ID { get; set; }

        [Column("rwk_Code")]
        public string rwk_Code { get; set; }

        [Column("rwk_MsgCode")]
        public string rwk_MsgCode { get; set; }

        [Column("rwk_DomainID")]
        public int? rwk_DomainID { get; set; }

    }
}