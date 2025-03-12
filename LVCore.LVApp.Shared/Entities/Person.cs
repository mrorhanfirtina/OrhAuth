namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_Person")]
    public class Person
    {
        [Column("per_ID", true)] // Primary Key
        public int per_ID { get; set; }

        [Column("per_Code")]
        public string per_Code { get; set; }

        [Column("per_FirstName")]
        public string per_FirstName { get; set; }

        [Column("per_LastName")]
        public string per_LastName { get; set; }

        [Column("per_CraftID")]
        public int? per_CraftID { get; set; }

        [Column("per_Address")]
        public string per_Address { get; set; }

        [Column("per_Area")]
        public string per_Area { get; set; }

        [Column("per_City")]
        public string per_City { get; set; }

        [Column("per_ZipCode")]
        public string per_ZipCode { get; set; }

        [Column("per_CountryID")]
        public int? per_CountryID { get; set; }

        [Column("per_Phone")]
        public string per_Phone { get; set; }

        [Column("per_Mobile")]
        public string per_Mobile { get; set; }

        [Column("per_Email")]
        public string per_Email { get; set; }

        [Column("per_Memo")]
        public string per_Memo { get; set; }

        [Column("per_ExternalLED")]
        public int? per_ExternalLED { get; set; }

        [Column("per_CompanyID")]
        public int? per_CompanyID { get; set; }

        [Column("per_DomainID")]
        public int? per_DomainID { get; set; }

        [Column("per_IDNumber")]
        public string per_IDNumber { get; set; }

        [Column("per_PassportNumber")]
        public string per_PassportNumber { get; set; }

        [Column("per_SmartCard")]
        public string per_SmartCard { get; set; }

        [Column("per_SecurityCardID")]
        public int? per_SecurityCardID { get; set; }

        [Column("per_CountryStateID")]
        public int? per_CountryStateID { get; set; }

    }
}