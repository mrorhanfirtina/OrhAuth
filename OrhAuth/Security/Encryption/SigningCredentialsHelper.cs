using Microsoft.IdentityModel.Tokens;

namespace OrhAuth.Security.Encryption
{
    /// <summary>
    /// Provides a helper method to create signing credentials for JWT token generation.
    /// </summary>
    public static class SigningCredentialsHelper
    {
        /// <summary>
        /// Creates signing credentials using the provided symmetric security key
        /// and HMAC SHA-512 as the signing algorithm.
        /// </summary>
        /// <param name="securityKey">The symmetric security key to be used for signing the token.</param>
        /// <returns>A <see cref="SigningCredentials"/> instance configured with HmacSha512Signature.</returns>
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
