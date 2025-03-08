namespace OrhAuth.Configurations
{
    public class OrhAuthOptions
    {
        public string ConnectionString { get; set; }
        public bool CreateDatabaseIfNotExists { get; set; } = true;
        public string TokenSecurityKey { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
        public int TokenExpirationMinutes { get; set; } = 30;
        public int RefreshTokenTTLDays { get; set; } = 7;
        public bool AddDefaultAdmin { get; set; } = true;
        public string DefaultAdminEmail { get; set; } = "admin@example.com";
        public string DefaultAdminPassword { get; set; } = "Admin123!";

        // Genişletilmiş User tipi için yeni özellik
        public System.Type ExtendedUserType { get; set; }
    }
}
