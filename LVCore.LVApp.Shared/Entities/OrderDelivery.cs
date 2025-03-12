namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderDelivery")]
    public class OrderDelivery
    {
        [Column("odv_ID", true)] // Primary Key
        public int odv_ID { get; set; }

        [Column("odv_OrderID")]
        public int? odv_OrderID { get; set; }

        [Column("odv_Contact")]
        public string odv_Contact { get; set; }

        [Column("odv_Address")]
        public string odv_Address { get; set; }

        [Column("odv_Area")]
        public string odv_Area { get; set; }

        [Column("odv_City")]
        public string odv_City { get; set; }

        [Column("odv_Country")]
        public string odv_Country { get; set; }

        [Column("odv_ZipCode")]
        public string odv_ZipCode { get; set; }

        [Column("odv_Phone")]
        public string odv_Phone { get; set; }

        [Column("odv_FAX")]
        public string odv_FAX { get; set; }

        [Column("odv_Email")]
        public string odv_Email { get; set; }

        [Column("odv_GeoSectorID")]
        public int? odv_GeoSectorID { get; set; }

        [Column("odv_DomainID")]
        public int? odv_DomainID { get; set; }

        [Column("odv_x")]
        public string odv_x { get; set; }

        [Column("odv_y")]
        public string odv_y { get; set; }

        [Column("odv_ZipRange")]
        public string odv_ZipRange { get; set; }

        [Column("odv_AddressNotes")]
        public string odv_AddressNotes { get; set; }

        [Column("odv_CountryState")]
        public string odv_CountryState { get; set; }

        [Column("odv_AddressClassID")]
        public int? odv_AddressClassID { get; set; }

    }
}