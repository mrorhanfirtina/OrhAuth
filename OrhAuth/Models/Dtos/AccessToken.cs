using System;

namespace OrhAuth.Models.Dtos
{
    /// <summary>
    /// Represents a JWT access token along with its expiration and associated refresh token.
    /// Used for authentication and session continuation in OrhAuth.
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// Gets or sets the JWT access token string.
        /// This token is used to authenticate requests.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the access token.
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Gets or sets the refresh token string used to obtain a new access token after expiration.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
