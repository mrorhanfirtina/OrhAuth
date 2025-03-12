namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("BI_PriceListObjects")]
    public class PriceListObjects
    {
        [Column("plo_ID", true)] // Primary Key
        public int plo_ID { get; set; }

        [Column("plo_ObjGroupID")]
        public int plo_ObjGroupID { get; set; }

        [Column("plo_DisplayPriority")]
        public int? plo_DisplayPriority { get; set; }

        [Column("plo_Description")]
        public string plo_Description { get; set; }

        [Column("plo_TableName")]
        public string plo_TableName { get; set; }

        [Column("plo_IDFieldName")]
        public string plo_IDFieldName { get; set; }

        [Column("plo_SelectCommand")]
        public string plo_SelectCommand { get; set; }

        [Column("plo_SelectCommandValues")]
        public string plo_SelectCommandValues { get; set; }

        [Column("plo_DomainID")]
        public int? plo_DomainID { get; set; }

        [Column("plo_GroupFieldName")]
        public string plo_GroupFieldName { get; set; }

    }
}