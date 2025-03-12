namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_LogisticSite")]
    public class LogisticSite
    {
        [Column("los_ID", true)] // Primary Key
        public int los_ID { get; set; }

        [Column("los_CompanyID")]
        public int? los_CompanyID { get; set; }

        [Column("los_Code")]
        public string los_Code { get; set; }

        [Column("los_Description")]
        public string los_Description { get; set; }

        [Column("los_Memo")]
        public string los_Memo { get; set; }

        [Column("los_Address")]
        public string los_Address { get; set; }

        [Column("los_Area")]
        public string los_Area { get; set; }

        [Column("los_City")]
        public string los_City { get; set; }

        [Column("los_CountryID")]
        public int? los_CountryID { get; set; }

        [Column("los_ZipCode")]
        public string los_ZipCode { get; set; }

        [Column("los_Phone")]
        public string los_Phone { get; set; }

        [Column("los_Fax")]
        public string los_Fax { get; set; }

        [Column("los_WMSLogisticUnitID")]
        public int? los_WMSLogisticUnitID { get; set; }

        [Column("los_DomainID")]
        public int? los_DomainID { get; set; }

        [Column("los_x")]
        public string los_x { get; set; }

        [Column("los_y")]
        public string los_y { get; set; }

        [Column("los_AddressNotes")]
        public string los_AddressNotes { get; set; }

        [Column("los_CountryStateID")]
        public int? los_CountryStateID { get; set; }

        [Column("los_AddressClassID")]
        public int? los_AddressClassID { get; set; }

        [Column("los_EmergencyPersonID")]
        public int? los_EmergencyPersonID { get; set; }

        [Column("los_EmergencyPhone")]
        public string los_EmergencyPhone { get; set; }

        [Column("los_EmergencyTitle")]
        public string los_EmergencyTitle { get; set; }

        [Column("los_InfMatPersonID")]
        public int? los_InfMatPersonID { get; set; }

        [Column("los_InfMatPhone")]
        public string los_InfMatPhone { get; set; }

        [Column("los_InfMatTitle")]
        public string los_InfMatTitle { get; set; }

        [Column("los_CurrencyID")]
        public int? los_CurrencyID { get; set; }

    }
}