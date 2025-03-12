namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_OrderReceiver")]
    public class OrderReceiver
    {
        [Column("orr_ID", true)] // Primary Key
        public int orr_ID { get; set; }

        [Column("orr_OrderID")]
        public int? orr_OrderID { get; set; }

        [Column("orr_Code")]
        public string orr_Code { get; set; }

        [Column("orr_FullName")]
        public string orr_FullName { get; set; }

        [Column("orr_ShortName")]
        public string orr_ShortName { get; set; }

        [Column("orr_Contact")]
        public string orr_Contact { get; set; }

        [Column("orr_Activity")]
        public string orr_Activity { get; set; }

        [Column("orr_AFM")]
        public string orr_AFM { get; set; }

        [Column("orr_DOY")]
        public string orr_DOY { get; set; }

        [Column("orr_Address")]
        public string orr_Address { get; set; }

        [Column("orr_Area")]
        public string orr_Area { get; set; }

        [Column("orr_City")]
        public string orr_City { get; set; }

        [Column("orr_Country")]
        public string orr_Country { get; set; }

        [Column("orr_ZipCode")]
        public string orr_ZipCode { get; set; }

        [Column("orr_Phone")]
        public string orr_Phone { get; set; }

        [Column("orr_FAX")]
        public string orr_FAX { get; set; }

        [Column("orr_Email")]
        public string orr_Email { get; set; }

        [Column("orr_DomainID")]
        public int? orr_DomainID { get; set; }

        [Column("orr_AddressNotes")]
        public string orr_AddressNotes { get; set; }

        [Column("orr_CountryState")]
        public string orr_CountryState { get; set; }

        [Column("orr_AddressClassID")]
        public int? orr_AddressClassID { get; set; }

    }
}