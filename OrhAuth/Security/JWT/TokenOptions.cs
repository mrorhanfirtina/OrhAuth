namespace OrhAuth.Security.JWT
{
    /// <summary>
    /// Represents the configuration settings required for generating and validating JWT tokens.
    /// </summary>
    public class TokenOptions
    {
        /// <summary>
        /// Gets or sets the intended audience of the token (typically the client application).
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the token (typically the API or authentication server).
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the expiration time of the access token in minutes.
        /// </summary>
        public int AccessTokenExpiration { get; set; }

        /// <summary>
        /// Gets or sets the secret key used to sign the token.
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// Gets or sets the time-to-live (TTL) of the refresh token in days.
        /// </summary>
        public int RefreshTokenTTL { get; set; }
    }
}
