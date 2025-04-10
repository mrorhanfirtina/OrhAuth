using Microsoft.IdentityModel.Tokens;
using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using OrhAuth.Security.Encryption;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using System.Linq;
using OrhAuth.Attributes;

namespace OrhAuth.Security.JWT
{
    /// <summary>
    /// Helper class responsible for creating, signing, and validating JWT tokens used for authentication and authorization.
    /// </summary>
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        /// <summary>
        /// Initializes a new instance of <see cref="JwtHelper"/> using configuration values from App.config or Web.config.
        /// </summary>
        public JwtHelper()
        {
            _tokenOptions = new TokenOptions
            {
                Audience = ConfigurationManager.AppSettings["Jwt:Audience"],
                Issuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
                AccessTokenExpiration = Convert.ToInt32(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiration"]),
                SecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
                RefreshTokenTTL = Convert.ToInt32(ConfigurationManager.AppSettings["Jwt:RefreshTokenTTL"]),
            };
        }

        /// <summary>
        /// Initializes a new instance of <see cref="JwtHelper"/> using specified parameters.
        /// </summary>
        /// <param name="securityKey">Signing key for token generation.</param>
        /// <param name="issuer">Token issuer (usually API).</param>
        /// <param name="audience">Token audience (usually frontend).</param>
        /// <param name="expirationMinutes">Access token expiration time in minutes.</param>
        public JwtHelper(string securityKey, string issuer, string audience, int expirationMinutes)
        {
            _tokenOptions = new TokenOptions
            {
                SecurityKey = securityKey,
                Issuer = issuer,
                Audience = audience,
                AccessTokenExpiration = expirationMinutes,
                RefreshTokenTTL = 7 // Default 7 days
            };
        }

        /// <summary>
        /// Creates an access token for the specified user and roles.
        /// </summary>
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = CreateRefreshToken()
            };
        }


        /// <summary>
        /// Creates a secure random refresh token.
        /// </summary>
        /// <returns>A base64-encoded refresh token string.</returns>
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        /// <summary>
        /// Creates a <see cref="JwtSecurityToken"/> for the specified user and their associated operation claims.
        /// </summary>
        /// <param name="tokenOptions">The token configuration options including issuer, audience, expiration, and signing key.</param>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <param name="signingCredentials">The signing credentials used to sign the token.</param>
        /// <param name="operationClaims">A list of roles or permissions associated with the user.</param>
        /// <returns>A JWT token including the user's identity and roles.</returns>
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var claims = SetClaims(user, operationClaims);
            return CreateJwtSecurityToken(tokenOptions, claims, signingCredentials);
        }


        /// <summary>
        /// Creates a <see cref="JwtSecurityToken"/> directly from a list of claims.
        /// </summary>
        /// <param name="tokenOptions">The token configuration options including issuer, audience, expiration, and signing key.</param>
        /// <param name="claims">The claims to be embedded in the token.</param>
        /// <param name="signingCredentials">The signing credentials used to sign the token.</param>
        /// <returns>A signed JWT token that contains the provided claims.</returns>
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
            IEnumerable<Claim> claims, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: signingCredentials
            );

            return jwt;
        }


        /// <summary>
        /// Generates a collection of <see cref="Claim"/> objects based on the user's identity and assigned roles.
        /// It also includes additional claims for extended user types marked with the <see cref="AddToClaimAttribute"/>.
        /// </summary>
        /// <param name="user">The user entity containing identity and extended information.</param>
        /// <param name="operationClaims">A list of roles (operation claims) assigned to the user.</param>
        /// <returns>A list of claims representing the user's identity, roles, and optionally extended properties.</returns>
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName ?? ""} {user.LastName ?? ""}".Trim()));

            if (!string.IsNullOrEmpty(user.LocalityId))
            {
                claims.Add(new Claim(ClaimTypes.Locality, user.LocalityId));
            }

            claims.Add(new Claim("UserId", user.Id.ToString()));

            if (user.GetType() != typeof(User))
            {
                var userType = user.GetType();
                var properties = userType.GetProperties();

                foreach (var property in properties)
                {
                    var addToClaimAttr = property.GetCustomAttributes(typeof(AddToClaimAttribute), false)
                        .FirstOrDefault() as AddToClaimAttribute;

                    if (addToClaimAttr != null)
                    {
                        var value = property.GetValue(user);
                        if (value != null)
                        {
                            string claimName = !string.IsNullOrEmpty(addToClaimAttr.ClaimName)
                                ? addToClaimAttr.ClaimName
                                : $"{addToClaimAttr.Prefix}{property.Name}";

                            if (value.GetType() != typeof(byte[]))
                            {
                                claims.Add(new Claim(claimName, value.ToString()));
                            }
                        }
                    }
                }
            }

            if (operationClaims != null)
            {
                foreach (var claim in operationClaims)
                {
                    if (claim != null && !string.IsNullOrEmpty(claim.Name))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, claim.Name));
                    }
                }
            }

            return claims;
        }


        /// <summary>
        /// Validates the specified JWT token using the configured token options.
        /// Checks signature, issuer, audience, and expiration date.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>
        /// <c>true</c> if the token is valid according to the configured security settings; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _tokenOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a JWT access token for the specified user, including their roles and any additional claims.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <param name="operationClaims">The list of operation claims (roles/permissions) assigned to the user.</param>
        /// <param name="additionalClaims">A collection of additional custom claims to be included in the token.</param>
        /// <returns>
        /// An <see cref="AccessToken"/> object containing the generated JWT, its expiration time, and a refresh token.
        /// </returns>
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims, IEnumerable<Claim> additionalClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            // Temel claim'leri oluştur
            var claims = SetClaims(user, operationClaims).ToList();

            // Ek claim'leri ekle
            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            var jwt = CreateJwtSecurityToken(_tokenOptions, claims, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = CreateRefreshToken()
            };
        }

        /// <summary>
        /// Creates a JWT access token for the specified user, including their roles and additional claims provided as a dictionary.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <param name="operationClaims">The list of operation claims (roles/permissions) assigned to the user.</param>
        /// <param name="additionalClaims">A dictionary of additional claims to include in the token, where the key is the claim type and the value is the claim value.</param>
        /// <returns>
        /// An <see cref="AccessToken"/> object containing the generated JWT, its expiration time, and a refresh token.
        /// </returns>
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims, Dictionary<string, string> additionalClaims)
        {
            // Dictionary'yi Claim listesine dönüştür
            var additionalClaimsList = additionalClaims?.Select(c => new Claim(c.Key, c.Value ?? "")).ToList();

            // Diğer overload'ı çağır
            return CreateToken(user, operationClaims, additionalClaimsList);
        }
    }
}
