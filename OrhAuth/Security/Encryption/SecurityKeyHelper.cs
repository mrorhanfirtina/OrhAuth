using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrhAuth.Security.Encryption
{
    /// <summary>
    /// Provides utility methods for generating security keys used in token-based authentication.
    /// </summary>
    public static class SecurityKeyHelper
    {
        /// <summary>
        /// Creates a symmetric security key from the specified string.
        /// This key is typically used for signing JWT tokens.
        /// </summary>
        /// <param name="securityKey">The security key string, must be sufficiently strong (e.g., at least 32 characters).</param>
        /// <returns>A SymmetricSecurityKey instance based on the provided string.</returns>
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
