namespace OrhAuth.Configurations
{
    /// <summary>
    /// Configuration options for the OrhAuth authentication and authorization framework.
    /// Used to initialize database connection, token settings, default admin user, and extended user type.
    /// </summary>
    public class OrhAuthOptions
    {
        /// <summary>
        /// The connection string to the authentication database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Indicates whether the database should be automatically created if it does not exist.
        /// Default is <c>true</c>.
        /// </summary>
        public bool CreateDatabaseIfNotExists { get; set; } = true;

        /// <summary>
        /// The secret key used to sign JWT tokens. Must be at least 32 characters for security.
        /// </summary>
        public string TokenSecurityKey { get; set; }

        /// <summary>
        /// The issuer to be embedded in the JWT token.
        /// </summary>
        public string TokenIssuer { get; set; }

        /// <summary>
        /// The audience to be embedded in the JWT token.
        /// </summary>
        public string TokenAudience { get; set; }

        /// <summary>
        /// Token expiration duration in minutes. Default is 30.
        /// </summary>
        public int TokenExpirationMinutes { get; set; } = 30;

        /// <summary>
        /// Refresh token time-to-live in days. Default is 7.
        /// </summary>
        public int RefreshTokenTTLDays { get; set; } = 7;

        /// <summary>
        /// Indicates whether a default admin user should be created when the database is initialized.
        /// Default is <c>true</c>.
        /// </summary>
        public bool AddDefaultAdmin { get; set; } = true;

        /// <summary>
        /// The email address of the default admin user. Default is "admin@example.com".
        /// </summary>
        public string DefaultAdminEmail { get; set; } = "admin@example.com";

        /// <summary>
        /// The password of the default admin user. Default is "Admin123!".
        /// </summary>
        public string DefaultAdminPassword { get; set; } = "Admin123!";

        /// <summary>
        /// Specifies the custom user type that extends the base <c>User</c> class.
        /// Used to add additional properties to the <c>Users</c> table dynamically.
        /// </summary>
        public System.Type ExtendedUserType { get; set; }
    }
}
