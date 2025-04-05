using OrhAuth.Models.Entities.Base;
using System;
using System.Collections.Generic;

namespace OrhAuth.Models.Entities
{
    /// <summary>
    /// Represents a user within the authentication and authorization system.
    /// Includes identity information, credentials, and related navigation properties.
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address. This must be unique and is used for login.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's optional username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the hashed representation of the user's password.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the salt used for hashing the password.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Indicates whether the user is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the locality identifier for the user (used for tenant or regional filtering).
        /// </summary>
        public string LocalityId { get; set; }

        /// <summary>
        /// Gets or sets the token used to reset the user's password.
        /// </summary>
        public string PasswordResetToken { get; set; }

        /// <summary>
        /// Gets or sets the salt for the password reset token.
        /// </summary>
        public string PasswordResetTokenSalt { get; set; }

        /// <summary>
        /// Gets or sets the expiration date for the password reset token.
        /// </summary>
        public DateTime? PasswordResetTokenExpiry { get; set; }

        /// <summary>
        /// Navigation property for user-role assignments.
        /// </summary>
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        /// <summary>
        /// Navigation property for refresh tokens associated with this user.
        /// </summary>
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Initializes collections for navigation properties.
        /// </summary>
        public User()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
            RefreshTokens = new HashSet<RefreshToken>();
        }
    }
}
