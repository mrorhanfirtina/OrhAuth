namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_Machine")]
    public class Machine
    {
        [Column("mch_ID", true)] // Primary Key
        public int mch_ID { get; set; }

        [Column("mch_Code")]
        public string mch_Code { get; set; }

        [Column("mch_Description")]
        public string mch_Description { get; set; }

        [Column("mch_ModelID")]
        public int? mch_ModelID { get; set; }

        [Column("mch_StatusID")]
        public int? mch_StatusID { get; set; }

        [Column("mch_LocationID")]
        public int? mch_LocationID { get; set; }

        [Column("mch_LogisticUnitID")]
        public int? mch_LogisticUnitID { get; set; }

        [Column("mch_DomainID")]
        public int? mch_DomainID { get; set; }

    }
}