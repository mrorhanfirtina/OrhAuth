namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_ProductBarcode")]
    public class ProductBarcode
    {
        [Column("pbc_ID", true)] // Primary Key
        public int pbc_ID { get; set; }

        [Column("pbc_ProductID")]
        public int? pbc_ProductID { get; set; }

        [Column("pbc_String")]
        public string pbc_String { get; set; }

        [Column("pbc_ItemUnitID")]
        public int? pbc_ItemUnitID { get; set; }

        [Column("pbc_BarcodeTypeID")]
        public int? pbc_BarcodeTypeID { get; set; }

        [Column("pbc_Multiplier")]
        public int? pbc_Multiplier { get; set; }

        [Column("pbc_DomainID")]
        public int? pbc_DomainID { get; set; }

        [Column("pbc_UseInContLabelLED")]
        public int? pbc_UseInContLabelLED { get; set; }

        [Column("pbc_Memo")]
        public string pbc_Memo { get; set; }

    }
}