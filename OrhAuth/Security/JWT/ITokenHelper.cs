using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace OrhAuth.Security.JWT
{
    /// <summary>
    /// Defines methods for creating and validating JSON Web Tokens (JWT) and refresh tokens.
    /// </summary>
    public interface ITokenHelper
    {
        /// <summary>
        /// Creates a JWT access token for the specified user with associated operation claims.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <param name="operationClaims">List of operation claims (roles/permissions) to include in the token.</param>
        /// <returns>An access token containing the JWT and its metadata.</returns>
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);

        /// <summary>
        /// Creates a JWT access token with additional claims provided as a list of Claim objects.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <param name="operationClaims">List of operation claims (roles/permissions) to include in the token.</param>
        /// <param name="additionalClaims">Custom claims to include in the token.</param>
        /// <returns>An access token containing the JWT and its metadata.</returns>
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, IEnumerable<Claim> additionalClaims);

        /// <summary>
        /// Creates a JWT access token with additional claims provided as key-value pairs.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <param name="operationClaims">List of operation claims (roles/permissions) to include in the token.</param>
        /// <param name="additionalClaims">Custom claims as key-value pairs to include in the token.</param>
        /// <returns>An access token containing the JWT and its metadata.</returns>
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, Dictionary<string, string> additionalClaims);

        /// <summary>
        /// Generates a new refresh token as a unique secure string.
        /// </summary>
        /// <returns>A newly generated refresh token string.</returns>
        string CreateRefreshToken();

        /// <summary>
        /// Validates whether the provided JWT token is well-formed and not expired or tampered.
        /// </summary>
        /// <param name="token">The JWT token string to validate.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        bool ValidateToken(string token);
    }
}
