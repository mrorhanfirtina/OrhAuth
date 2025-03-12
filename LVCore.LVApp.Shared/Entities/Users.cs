namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;
    using System;

    [Table("LV_Users")]
    public class Users
    {
        [Column("usr_ID", true)] // Primary Key
        public int usr_ID { get; set; }

        [Column("usr_Login")]
        public string usr_Login { get; set; }

        [Column("usr_PersonID")]
        public int? usr_PersonID { get; set; }

        [Column("usr_Password")]
        public string usr_Password { get; set; }

        [Column("usr_LanguageID")]
        public int? usr_LanguageID { get; set; }

        [Column("usr_LogisticSiteID")]
        public int? usr_LogisticSiteID { get; set; }

        [Column("usr_AllDepositorsLED")]
        public int? usr_AllDepositorsLED { get; set; }

        [Column("usr_AllLSLED")]
        public int? usr_AllLSLED { get; set; }

        [Column("usr_DomainID")]
        public int usr_DomainID { get; set; }

        [Column("usr_EncryptedLED")]
        public int? usr_EncryptedLED { get; set; }

        [Column("usr_PasswordChangedDate")]
        public DateTime? usr_PasswordChangedDate { get; set; }

        [Column("usr_VoicePassword")]
        public string usr_VoicePassword { get; set; }

        [Column("usr_AllVProfilesLED")]
        public int? usr_AllVProfilesLED { get; set; }

        [Column("usr_VoiceProfileVersionID")]
        public int? usr_VoiceProfileVersionID { get; set; }

        [Column("usr_Folder")]
        public string usr_Folder { get; set; }

        [Column("usr_RFFolder")]
        public string usr_RFFolder { get; set; }

        [Column("usr_FormFolder")]
        public string usr_FormFolder { get; set; }

        [Column("usr_AllowFormModificationLED")]
        public int? usr_AllowFormModificationLED { get; set; }

        [Column("usr_UserValidationType")]
        public int? usr_UserValidationType { get; set; }

        [Column("usr_LogisticUnitID")]
        public int? usr_LogisticUnitID { get; set; }

        [Column("usr_IdleTimeLimit")]
        public int? usr_IdleTimeLimit { get; set; }

        [Column("usr_IdleTimeoutAction")]
        public int? usr_IdleTimeoutAction { get; set; }

        [Column("usr_logErrorsLED")]
        public int? usr_logErrorsLED { get; set; }

        [Column("usr_InactiveLED")]
        public int? usr_InactiveLED { get; set; }

        [Column("usr_AllDashboardsLED")]
        public int? usr_AllDashboardsLED { get; set; }

        [Column("usr_UserTypeID")]
        public int? usr_UserTypeID { get; set; }

    }
}