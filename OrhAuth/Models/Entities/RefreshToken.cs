using OrhAuth.Models.Entities.Base;
using System;

namespace OrhAuth.Models.Entities
{
    /// <summary>
    /// Represents a refresh token issued to a user for renewing access tokens without requiring re-authentication.
    /// </summary>
    public class RefreshToken : EntityBase
    {
        /// <summary>
        /// Gets or sets the ID of the user to whom the refresh token belongs.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the refresh token string.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the refresh token.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets or sets the IP address from which the token was created.
        /// </summary>
        public string CreatedByIp { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the token was revoked, if applicable.
        /// </summary>
        public DateTime? Revoked { get; set; }

        /// <summary>
        /// Gets or sets the IP address from which the token was revoked.
        /// </summary>
        public string RevokedByIp { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the token was revoked.
        /// </summary>
        public DateTime? RevokedDate { get; set; }

        /// <summary>
        /// Gets or sets the token that replaced this token, if applicable.
        /// </summary>
        public string ReplacedByToken { get; set; }

        /// <summary>
        /// Navigation property for the associated user.
        /// </summary>
        public virtual User User { get; set; }
    }
}
