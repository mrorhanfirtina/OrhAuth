namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_CustomerDelivery")]
    public class CustomerDelivery
    {
        [Column("cud_Id", true)] // Primary Key
        public int cud_Id { get; set; }

        [Column("cud_CustomerID")]
        public int? cud_CustomerID { get; set; }

        [Column("cud_Contact")]
        public string cud_Contact { get; set; }

        [Column("cud_Address")]
        public string cud_Address { get; set; }

        [Column("cud_Area")]
        public string cud_Area { get; set; }

        [Column("cud_City")]
        public string cud_City { get; set; }

        [Column("cud_CountryID")]
        public int? cud_CountryID { get; set; }

        [Column("cud_ZipCode")]
        public string cud_ZipCode { get; set; }

        [Column("cud_Phone")]
        public string cud_Phone { get; set; }

        [Column("cud_Fax")]
        public string cud_Fax { get; set; }

        [Column("cud_Email")]
        public string cud_Email { get; set; }

        [Column("cud_GeoSectorID")]
        public int? cud_GeoSectorID { get; set; }

        [Column("cud_DomainID")]
        public int? cud_DomainID { get; set; }

        [Column("cud_x")]
        public string cud_x { get; set; }

        [Column("cud_y")]
        public string cud_y { get; set; }

        [Column("cud_ZipRange")]
        public string cud_ZipRange { get; set; }

        [Column("cud_AddressNotes")]
        public string cud_AddressNotes { get; set; }

        [Column("cud_CountryStateID")]
        public int? cud_CountryStateID { get; set; }

        [Column("cud_AddressClassID")]
        public int? cud_AddressClassID { get; set; }

    }
}