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

namespace OrhAuth.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

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

        // Parametreli constructor
        public JwtHelper(string securityKey, string issuer, string audience, int expirationMinutes)
        {
            _tokenOptions = new TokenOptions
            {
                SecurityKey = securityKey,
                Issuer = issuer,
                Audience = audience,
                AccessTokenExpiration = expirationMinutes,
                RefreshTokenTTL = 7 // Varsayılan 7 gün
            };
        }

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

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.Locality, user.LocalityId));

            operationClaims.ForEach(claim =>
            {
                claims.Add(new Claim(ClaimTypes.Role, claim.Name));
            });

            return claims;
        }
    }
}
