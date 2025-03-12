namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ItemPackTypeHierarchy")]
    public class ItemPackTypeHierarchy
    {
        [Column("iph_ID", true)] // Primary Key
        public int iph_ID { get; set; }

        [Column("iph_ParentItemUnitID")]
        public int? iph_ParentItemUnitID { get; set; }

        [Column("iph_ChildItemUnitID")]
        public int? iph_ChildItemUnitID { get; set; }

        [Column("iph_Quantity")]
        public decimal? iph_Quantity { get; set; }

        [Column("iph_Layers")]
        public int? iph_Layers { get; set; }

        [Column("iph_DomainID")]
        public int? iph_DomainID { get; set; }

    }
}