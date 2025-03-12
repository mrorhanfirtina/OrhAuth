namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Company")]
    public class Company
    {
        [Column("cmp_ID", true)] // Primary Key
        public int cmp_ID { get; set; }

        [Column("cmp_Code")]
        public string cmp_Code { get; set; }

        [Column("cmp_FullName")]
        public string cmp_FullName { get; set; }

        [Column("cmp_ShortName")]
        public string cmp_ShortName { get; set; }

        [Column("cmp_Responsible")]
        public string cmp_Responsible { get; set; }

        [Column("cmp_Activity")]
        public string cmp_Activity { get; set; }

        [Column("cmp_DOYID")]
        public int? cmp_DOYID { get; set; }

        [Column("cmp_AFM")]
        public string cmp_AFM { get; set; }

        [Column("cmp_Address")]
        public string cmp_Address { get; set; }

        [Column("cmp_Area")]
        public string cmp_Area { get; set; }

        [Column("cmp_City")]
        public string cmp_City { get; set; }

        [Column("cmp_CountryID")]
        public int? cmp_CountryID { get; set; }

        [Column("cmp_ZipCode")]
        public string cmp_ZipCode { get; set; }

        [Column("cmp_Phone")]
        public string cmp_Phone { get; set; }

        [Column("cmp_Fax")]
        public string cmp_Fax { get; set; }

        [Column("cmp_Email")]
        public string cmp_Email { get; set; }

        [Column("cmp_DomainID")]
        public int? cmp_DomainID { get; set; }

        [Column("cmp_LastUpdateTime")]
        public DateTime? cmp_LastUpdateTime { get; set; }

        [Column("cmp_DUNSNumber")]
        public string cmp_DUNSNumber { get; set; }

        [Column("cmp_AddressNotes")]
        public string cmp_AddressNotes { get; set; }

        [Column("cmp_CountryStateID")]
        public int? cmp_CountryStateID { get; set; }

        [Column("cmp_AddressClassID")]
        public int? cmp_AddressClassID { get; set; }

    }
}