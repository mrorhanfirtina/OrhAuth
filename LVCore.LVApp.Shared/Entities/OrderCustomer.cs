namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderCustomer")]
    public class OrderCustomer
    {
        [Column("orc_ID", true)] // Primary Key
        public int orc_ID { get; set; }

        [Column("orc_OrderID")]
        public int? orc_OrderID { get; set; }

        [Column("orc_Code")]
        public string orc_Code { get; set; }

        [Column("orc_FullName")]
        public string orc_FullName { get; set; }

        [Column("orc_ShortName")]
        public string orc_ShortName { get; set; }

        [Column("orc_Contact")]
        public string orc_Contact { get; set; }

        [Column("orc_Activity")]
        public string orc_Activity { get; set; }

        [Column("orc_AFM")]
        public string orc_AFM { get; set; }

        [Column("orc_DOY")]
        public string orc_DOY { get; set; }

        [Column("orc_Address")]
        public string orc_Address { get; set; }

        [Column("orc_Area")]
        public string orc_Area { get; set; }

        [Column("orc_City")]
        public string orc_City { get; set; }

        [Column("orc_Country")]
        public string orc_Country { get; set; }

        [Column("orc_ZipCode")]
        public string orc_ZipCode { get; set; }

        [Column("orc_Phone")]
        public string orc_Phone { get; set; }

        [Column("orc_FAX")]
        public string orc_FAX { get; set; }

        [Column("orc_Email")]
        public string orc_Email { get; set; }

        [Column("orc_DomainID")]
        public int? orc_DomainID { get; set; }

        [Column("orc_AddressNotes")]
        public string orc_AddressNotes { get; set; }

        [Column("orc_CountryState")]
        public string orc_CountryState { get; set; }

        [Column("orc_AddressClassID")]
        public int? orc_AddressClassID { get; set; }

    }
}