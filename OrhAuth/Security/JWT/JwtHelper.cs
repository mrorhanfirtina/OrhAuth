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

        // JwtSecurityToken oluşturma metodunu güncelle
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var claims = SetClaims(user, operationClaims);
            return CreateJwtSecurityToken(tokenOptions, claims, signingCredentials);
        }

        // Yeni metot: Doğrudan claim listesi alan JwtSecurityToken oluşturma metodu
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

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();

            // Temel kullanıcı özellikleri için null kontrolü
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName ?? ""} {user.LastName ?? ""}".Trim()));

            if (!string.IsNullOrEmpty(user.LocalityId))
            {
                claims.Add(new Claim(ClaimTypes.Locality, user.LocalityId));
            }

            claims.Add(new Claim("UserId", user.Id.ToString()));

            // ExtendedUser için AddToClaim attribute'u ile işaretlenmiş özellikleri ekle
            if (user.GetType() != typeof(User))
            {
                var userType = user.GetType();
                var properties = userType.GetProperties();

                foreach (var property in properties)
                {
                    // AddToClaim özniteliğini kontrol et
                    var addToClaimAttr = property.GetCustomAttributes(typeof(AddToClaimAttribute), false)
                        .FirstOrDefault() as AddToClaimAttribute;

                    if (addToClaimAttr != null)
                    {
                        var value = property.GetValue(user);
                        if (value != null)
                        {
                            // Öznitelikte belirtilen claim adını veya property adını kullan
                            string claimName = !string.IsNullOrEmpty(addToClaimAttr.ClaimName)
                                ? addToClaimAttr.ClaimName
                                : $"{addToClaimAttr.Prefix}{property.Name}";

                            // byte[] tipindeki özellikleri atla
                            if (value.GetType() != typeof(byte[]))
                            {
                                claims.Add(new Claim(claimName, value.ToString()));
                            }
                        }
                    }
                }
            }

            // Kullanıcı rolleri için null kontrolü
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

        // JwtHelper sınıfına eklenecek metod
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

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims, Dictionary<string, string> additionalClaims)
        {
            // Dictionary'yi Claim listesine dönüştür
            var additionalClaimsList = additionalClaims?.Select(c => new Claim(c.Key, c.Value ?? "")).ToList();

            // Diğer overload'ı çağır
            return CreateToken(user, operationClaims, additionalClaimsList);
        }
    }
}
