namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("LV_TransactionTypeScript")]
    public class TransactionTypeScript
    {
        [Column("tts_ID", true)] // Primary Key
        public int tts_ID { get; set; }

        [Column("tts_TransactionTypeID")]
        public int? tts_TransactionTypeID { get; set; }

        [Column("tts_Script")]
        public object tts_Script { get; set; }

        [Column("tts_DomainID")]
        public int? tts_DomainID { get; set; }

        [Column("tts_AssemblyName")]
        public string tts_AssemblyName { get; set; }

        [Column("tts_TypeName")]
        public string tts_TypeName { get; set; }

        [Column("tts_RawAssemblyName")]
        public string tts_RawAssemblyName { get; set; }

        [Column("tts_RawTypeName")]
        public string tts_RawTypeName { get; set; }

        [Column("tts_SavedAssemblyName")]
        public string tts_SavedAssemblyName { get; set; }

        [Column("tts_SavedTypeName")]
        public string tts_SavedTypeName { get; set; }

    }
}