namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_LSUserAccess")]
    public class LSUserAccess
    {
        [Column("lua_ID", true)] // Primary Key
        public int lua_ID { get; set; }

        [Column("lua_LogisticSiteID")]
        public int? lua_LogisticSiteID { get; set; }

        [Column("lua_UserID")]
        public int? lua_UserID { get; set; }

        [Column("lua_UpdateLED")]
        public int? lua_UpdateLED { get; set; }

        [Column("lua_DomainID")]
        public int? lua_DomainID { get; set; }

    }
}