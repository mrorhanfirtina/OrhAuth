using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Abstract;
using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using OrhAuth.Security.Hashing;
using OrhAuth.Security.JWT;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace OrhAuth.Services
{
    public class AuthManager : IAuthService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<OperationClaim> _operationClaimRepository;
        private readonly IEntityRepository<UserOperationClaim> _userOperationClaimRepository;
        private readonly IEntityRepository<RefreshToken> _refreshTokenRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly string _connectionString;

        public AuthManager(
            IEntityRepository<User> userRepository,
            IEntityRepository<OperationClaim> operationClaimRepository,
            IEntityRepository<UserOperationClaim> userOperationClaimRepository,
            IEntityRepository<RefreshToken> refreshTokenRepository,
            ITokenHelper tokenHelper, string connectionString)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenHelper = tokenHelper;
            _connectionString = connectionString;
        }

        #region Mevcut Metodlar

        public User Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
                LocalityId = userForRegisterDto.LocalityId
            };

            _userRepository.Add(user);
            return user;
        }

        public User Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userRepository.Get(u => u.Email == userForLoginDto.Email);
            if (userToCheck == null)
                throw new Exception("Kullanıcı bulunamadı");

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                throw new Exception("Parola hatası");

            return userToCheck;
        }

        public bool UserExists(string email)
        {
            return _userRepository.Get(u => u.Email == email) != null;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = accessToken.RefreshToken,
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = "127.0.0.1" // Gerçek projede istemcinin IP'sini almalısınız
            };

            // Refresh token'ı kaydet
            _refreshTokenRepository.Add(refreshToken);

            return accessToken;
        }

        public AccessToken CreateAccessToken(User user, Dictionary<string, string> customClaims = null)
        {
            var claims = GetClaims(user);

            // Eğer özel claim'ler varsa, bunları kullanarak token oluştur
            AccessToken accessToken;
            if (customClaims != null && customClaims.Count > 0)
            {
                accessToken = _tokenHelper.CreateToken(user, claims, customClaims);
            }
            else
            {
                accessToken = _tokenHelper.CreateToken(user, claims);
            }

            // Refresh token işlemleri...
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = accessToken.RefreshToken,
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = "127.0.0.1" // Gerçek projede istemcinin IP'sini almalısınız
            };

            // Refresh token'ı kaydet
            _refreshTokenRepository.Add(refreshToken);

            return accessToken;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var userOperationClaims = _userOperationClaimRepository.GetList(uoc => uoc.UserId == user.Id);

            var operationClaims = new List<OperationClaim>();
            foreach (var userOperationClaim in userOperationClaims)
            {
                var operationClaim = _operationClaimRepository.Get(oc => oc.Id == userOperationClaim.OperationClaimId);
                operationClaims.Add(operationClaim);
            }

            return operationClaims;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        }

        #endregion

        #region Token Yönetimi

        public AccessToken RefreshToken(string refreshToken)
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken && rt.RevokedDate == null);
            if (token == null)
                throw new Exception("Geçersiz refresh token");

            // Token süresi dolmuş mu?
            if (token.Expires < DateTime.Now)
            {
                // Refresh token'ı iptal et
                token.RevokedDate = DateTime.Now;
                token.Revoked = DateTime.Now;
                _refreshTokenRepository.Update(token);
                throw new Exception("Refresh token süresi dolmuş");
            }

            var user = _userRepository.Get(u => u.Id == token.UserId);
            if (user == null || !user.IsActive)
                throw new Exception("Kullanıcı bulunamadı veya pasif durumda");

            // Yeni token oluştur
            var accessToken = _tokenHelper.CreateToken(user, GetClaims(user));

            // Eski refresh token'ı iptal et
            token.RevokedDate = DateTime.Now;
            token.Revoked = DateTime.Now;
            token.ReplacedByToken = accessToken.RefreshToken;
            _refreshTokenRepository.Update(token);

            // Yeni refresh token oluştur
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = accessToken.RefreshToken,
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = "127.0.0.1" // Gerçek projede istemcinin IP'sini almalısınız
            };
            _refreshTokenRepository.Add(newRefreshToken);

            return accessToken;
        }

        public AccessToken RefreshToken(string refreshToken, Dictionary<string, string> customClaims = null)
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken && rt.RevokedDate == null);
            if (token == null)
                throw new Exception("Geçersiz refresh token");

            // Token süresi dolmuş mu?
            if (token.Expires < DateTime.Now)
            {
                // Refresh token'ı iptal et
                token.RevokedDate = DateTime.Now;
                token.Revoked = DateTime.Now;
                _refreshTokenRepository.Update(token);
                throw new Exception("Refresh token süresi dolmuş");
            }

            var user = _userRepository.Get(u => u.Id == token.UserId);
            if (user == null || !user.IsActive)
                throw new Exception("Kullanıcı bulunamadı veya pasif durumda");

            // Kullanıcı yetkilerini al
            var claims = GetClaims(user);

            // Token oluştur
            AccessToken accessToken;

            // Eğer özel claim'ler varsa, bunları kullanarak token oluştur
            if (customClaims != null && customClaims.Count > 0)
            {
                accessToken = _tokenHelper.CreateToken(user, claims, customClaims);
            }
            else
            {
                // Özel claim yoksa, standart token oluştur
                accessToken = _tokenHelper.CreateToken(user, claims);
            }

            // Eski refresh token'ı iptal et
            token.RevokedDate = DateTime.Now;
            token.Revoked = DateTime.Now;
            token.ReplacedByToken = accessToken.RefreshToken;
            _refreshTokenRepository.Update(token);

            // Yeni refresh token oluştur
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = accessToken.RefreshToken,
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = "127.0.0.1" // Gerçek projede istemcinin IP'sini almalısınız
            };
            _refreshTokenRepository.Add(newRefreshToken);

            return accessToken;
        }

        public int GetUserIdByRefreshToken(string refreshToken)
        {
            // Refresh token'ı bul
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken && rt.RevokedDate == null);
            if (token == null)
                return 0;

            // Token süresi dolmuş mu?
            if (token.Expires < DateTime.Now)
            {
                // Refresh token'ı iptal et
                token.RevokedDate = DateTime.Now;
                _refreshTokenRepository.Update(token);
                return 0;
            }

            // Kullanıcı ID'sini döndür
            return token.UserId;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                return _tokenHelper.ValidateToken(token);
            }
            catch
            {
                return false;
            }
        }

        public bool RevokeToken(string refreshToken)
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken);
            if (token == null)
                return false;

            // Refresh token'ı iptal et
            token.RevokedDate = DateTime.Now;
            _refreshTokenRepository.Update(token);
            return true;
        }

        #endregion

        #region Kullanıcı Yönetimi

        public User GetUserById(int userId)
        {
            return _userRepository.Get(u => u.Id == userId);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _userRepository.Get(u => u.Id == user.Id);
            if (existingUser == null)
                return false;

            // Temel alanları güncelle
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.LocalityId = user.LocalityId;

            _userRepository.Update(existingUser);
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null)
                return false;

            // Kullanıcıya ait tüm refresh token'ları iptal et
            var refreshTokens = _refreshTokenRepository.GetList(rt => rt.UserId == userId && rt.RevokedDate == null);
            foreach (var token in refreshTokens)
            {
                token.RevokedDate = DateTime.Now;
                _refreshTokenRepository.Update(token);
            }

            // Kullanıcıya ait yetkileri temizle
            var userClaims = _userOperationClaimRepository.GetList(uoc => uoc.UserId == userId);
            foreach (var claim in userClaims)
            {
                _userOperationClaimRepository.Delete(claim);
            }

            // Kullanıcıyı sil
            _userRepository.Delete(user);
            return true;
        }

        public bool SetUserStatus(int userId, bool isActive)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null)
                return false;

            user.IsActive = isActive;
            _userRepository.Update(user);
            return true;
        }

        public List<User> GetUsers(int pageNumber, int pageSize)
        {
            // Sayfalama için Skip ve Take kullan
            return _userRepository.GetList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetUserCount()
        {
            return _userRepository.GetList().Count;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null)
                return false;

            // Eski şifreyi doğrula
            if (!HashingHelper.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
                return false;

            // Yeni şifre oluştur
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Update(user);
            return true;
        }

        public string RequestPasswordReset(string email)
        {
            var user = _userRepository.Get(u => u.Email == email);
            if (user == null)
                return "false";

            // Rastgele bir token oluştur
            var resetToken = Guid.NewGuid().ToString("N");

            // Token'ı hashle (güvenlik için)
            byte[] tokenHash, tokenSalt;
            HashingHelper.CreatePasswordHash(resetToken, out tokenHash, out tokenSalt);

            // User nesnesinde resetToken bilgisini sakla
            user.PasswordResetToken = Convert.ToBase64String(tokenHash);
            user.PasswordResetTokenSalt = Convert.ToBase64String(tokenSalt);
            user.PasswordResetTokenExpiry = DateTime.Now.AddHours(24); // 24 saat geçerli

            _userRepository.Update(user);

            // Gerçek uygulamada, burada email gönderme işlemi yapılır
            //Console.WriteLine($"Şifre sıfırlama bağlantısı: {resetToken}");

            return resetToken;
        }

        public bool ResetPassword(string resetToken, string newPassword)
        {
            // Tüm kullanıcıları kontrol etmek verimsiz olsa da
            // bu örnekte sadelik için bu şekilde yapıyoruz
            var users = _userRepository.GetList().ToList();

            foreach (var user in users)
            {
                // Kullanıcının token'ı var mı ve süresi geçmiş mi kontrol et
                if (string.IsNullOrEmpty(user.PasswordResetToken) ||
                    user.PasswordResetTokenExpiry < DateTime.Now)
                    continue;

                // Token'ı doğrula
                byte[] tokenHash = Convert.FromBase64String(user.PasswordResetToken);
                byte[] tokenSalt = Convert.FromBase64String(user.PasswordResetTokenSalt);

                if (HashingHelper.VerifyPasswordHash(resetToken, tokenHash, tokenSalt))
                {
                    // Token doğruysa şifreyi güncelle
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    // Reset token'ı temizle
                    user.PasswordResetToken = null;
                    user.PasswordResetTokenSalt = null;
                    user.PasswordResetTokenExpiry = null;

                    _userRepository.Update(user);
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Rol ve Yetki Yönetimi

        public OperationClaim CreateRole(string roleName)
        {
            // Rol zaten var mı?
            var existingRole = _operationClaimRepository.Get(oc => oc.Name == roleName);
            if (existingRole != null)
                return existingRole;

            var role = new OperationClaim { Name = roleName };
            _operationClaimRepository.Add(role);
            return role;
        }

        public bool DeleteRole(int roleId)
        {
            var role = _operationClaimRepository.Get(oc => oc.Id == roleId);
            if (role == null)
                return false;

            // Rol ile ilişkili tüm kullanıcı-rol ilişkilerini sil
            var userRoles = _userOperationClaimRepository.GetList(uoc => uoc.OperationClaimId == roleId);
            foreach (var userRole in userRoles)
            {
                _userOperationClaimRepository.Delete(userRole);
            }

            // Rolü sil
            _operationClaimRepository.Delete(role);
            return true;
        }

        public List<OperationClaim> GetRoles()
        {
            return _operationClaimRepository.GetList().ToList();
        }

        public bool AddClaim(int userId, string claimName)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            var claim = _operationClaimRepository.Get(oc => oc.Name == claimName);

            if (user == null || claim == null)
                return false;

            // Kullanıcı zaten bu yetkiye sahip mi?
            var existingClaim = _userOperationClaimRepository.Get(uoc =>
                uoc.UserId == userId && uoc.OperationClaimId == claim.Id);

            if (existingClaim != null)
                return true; // Zaten var, başarılı kabul ediyoruz

            // Kullanıcıya yetki ekle
            var userOperationClaim = new UserOperationClaim
            {
                UserId = userId,
                OperationClaimId = claim.Id
            };
            _userOperationClaimRepository.Add(userOperationClaim);
            return true;
        }

        public bool AssignRoleToUser(int userId, int roleId)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            var role = _operationClaimRepository.Get(oc => oc.Id == roleId);

            if (user == null || role == null)
                return false;

            // Kullanıcı zaten bu role sahip mi?
            var existingRole = _userOperationClaimRepository.Get(uoc =>
                uoc.UserId == userId && uoc.OperationClaimId == roleId);

            if (existingRole != null)
                return true; // Zaten var, başarılı kabul ediyoruz

            // Kullanıcıya rol ekle
            var userOperationClaim = new UserOperationClaim
            {
                UserId = userId,
                OperationClaimId = roleId
            };
            _userOperationClaimRepository.Add(userOperationClaim);
            return true;
        }

        public bool RemoveRoleFromUser(int userId, int roleId)
        {
            var userRole = _userOperationClaimRepository.Get(uoc =>
                uoc.UserId == userId && uoc.OperationClaimId == roleId);

            if (userRole == null)
                return false;

            _userOperationClaimRepository.Delete(userRole);
            return true;
        }

        public List<OperationClaim> GetUserRoles(int userId)
        {
            var userOperationClaims = _userOperationClaimRepository.GetList(uoc => uoc.UserId == userId);

            var operationClaims = new List<OperationClaim>();
            foreach (var userOperationClaim in userOperationClaims)
            {
                var operationClaim = _operationClaimRepository.Get(oc => oc.Id == userOperationClaim.OperationClaimId);
                operationClaims.Add(operationClaim);
            }

            return operationClaims;
        }

        public bool HasPermission(int userId, string operationName)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null || !user.IsActive)
                return false;

            var claim = _operationClaimRepository.Get(oc => oc.Name == operationName);
            if (claim == null)
                return false;

            return _userOperationClaimRepository.Get(uoc =>
                uoc.UserId == userId && uoc.OperationClaimId == claim.Id) != null;
        }

        public OperationClaim AddOperationClaim(string operationName)
        {
            // Yetki zaten var mı?
            var existingClaim = _operationClaimRepository.Get(oc => oc.Name == operationName);
            if (existingClaim != null)
                return existingClaim;

            var claim = new OperationClaim { Name = operationName };
            _operationClaimRepository.Add(claim);
            return claim;
        }

        public bool AssignClaimToRole(int roleId, int operationClaimId)
        {
            try
            {
                // Önce verilen IDlerin geçerli olup olmadığını kontrol et
                var role = _operationClaimRepository.Get(r => r.Id == roleId);
                var claim = _operationClaimRepository.Get(c => c.Id == operationClaimId);

                if (role == null || claim == null)
                    return false;

                // Bu roleId'ye sahip olan tüm kullanıcılara bu operationClaimId'yi ata

                // Bu role sahip tüm kullanıcıları bul
                var usersInRole = _userOperationClaimRepository.GetList(uoc => uoc.OperationClaimId == roleId).ToList();

                foreach (var userRole in usersInRole)
                {
                    // Kullanıcının bu yetkiye zaten sahip olup olmadığını kontrol et
                    var hasPermission = _userOperationClaimRepository.Get(
                        uoc => uoc.UserId == userRole.UserId && uoc.OperationClaimId == operationClaimId);

                    if (hasPermission == null)
                    {
                        // Kullanıcıya bu yetkiyi ekle
                        _userOperationClaimRepository.Add(new UserOperationClaim
                        {
                            UserId = userRole.UserId,
                            OperationClaimId = operationClaimId
                        });
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Genişletilmiş Kullanıcı İşlemleri

        public dynamic RegisterExtendedUser(UserForRegisterDto baseUser, object extendedProperties)
        {
            try
            {
                // Önce standart kullanıcı kaydını gerçekleştir
                var user = Register(baseUser);
                if (user == null || extendedProperties == null)
                    return user;

                // Genişletilmiş özellikleri SQL ile güncelle
                UpdateExtendedPropertiesWithSQL(user.Id, extendedProperties);

                // Güncellenmiş kullanıcıyı al
                return GetUserDynamicById(user.Id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RegisterExtendedUser hatası: {ex.Message}");
                return null;
            }
        }

        public bool UpdateExtendedUser(int userId, object extendedProperties)
        {
            try
            {
                var user = _userRepository.Get(u => u.Id == userId);
                if (user == null || extendedProperties == null)
                    return false;

                // Genişletilmiş özellikleri SQL ile güncelle
                return UpdateExtendedPropertiesWithSQL(userId, extendedProperties);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateExtendedUser hatası: {ex.Message}");
                return false;
            }
        }

        private bool UpdateExtendedPropertiesWithSQL(int userId, object extendedProperties)
        {
            try
            {
                // Güncellenecek özellikleri ve değerlerini topla
                var updateColumns = new Dictionary<string, object>();

                foreach (var property in extendedProperties.GetType().GetProperties())
                {
                    // Sadece ExtendUser özniteliği taşıyan özellikleri işle
                    if (property.IsDefined(typeof(Attributes.ExtendUserAttribute), false))
                    {
                        var value = property.GetValue(extendedProperties);
                        updateColumns.Add(property.Name, value);
                    }
                }

                if (updateColumns.Count == 0)
                    return false;

                // SQL UPDATE cümlesi oluştur
                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append("UPDATE Users SET ");

                var parameters = new List<SqlParameter>();
                var updateParts = new List<string>();

                foreach (var column in updateColumns)
                {
                    string paramName = $"@{column.Key}";
                    updateParts.Add($"{column.Key} = {paramName}");
                    parameters.Add(new SqlParameter(paramName, column.Value ?? DBNull.Value));
                }

                sqlBuilder.Append(string.Join(", ", updateParts));
                sqlBuilder.Append(" WHERE Id = @UserId");
                parameters.Add(new SqlParameter("@UserId", userId));

                // SQL komutunu çalıştır
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(sqlBuilder.ToString(), connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateExtendedPropertiesWithSQL hatası: {ex.Message}");
                return false;
            }
        }

        // Standart kullanıcıyı genişletilmiş tipe dönüştürmek için yardımcı metot
        private User ConvertToExtendedUser(User user, Type extendedUserType)
        {
            try
            {
                // Genişletilmiş tipte yeni bir nesne oluştur
                var extendedUser = Activator.CreateInstance(extendedUserType) as User;
                if (extendedUser == null)
                    return null;

                // Temel User özelliklerini kopyala
                foreach (var property in typeof(User).GetProperties())
                {
                    if (property.CanWrite)
                    {
                        var extendedProperty = extendedUserType.GetProperty(property.Name);
                        if (extendedProperty != null && extendedProperty.CanWrite)
                        {
                            var value = property.GetValue(user);
                            extendedProperty.SetValue(extendedUser, value);
                        }
                    }
                }

                // Standart kullanıcıyı sil
                _userRepository.Delete(user);

                // Genişletilmiş kullanıcıyı ekle
                _userRepository.Add(extendedUser);

                return extendedUser;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ConvertToExtendedUser hatası: {ex.Message}");
                return null;
            }
        }

        // Mevcut güncelleme mantığı (geriye dönük uyumluluk için)
        private bool UpdateStandardExtendedUser(User user, object extendedProperties)
        {
            try
            {
                foreach (var property in extendedProperties.GetType().GetProperties())
                {
                    // Sadece ExtendUser özniteliği taşıyan özellikleri işle
                    if (property.IsDefined(typeof(Attributes.ExtendUserAttribute), false))
                    {
                        var value = property.GetValue(extendedProperties);

                        // User sınıfındaki aynı isme sahip özelliği bul ve değerini ata
                        var userProperty = user.GetType().GetProperty(property.Name);
                        if (userProperty != null && userProperty.CanWrite)
                        {
                            userProperty.SetValue(user, value);
                        }
                    }
                }

                _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateStandardExtendedUser hatası: {ex.Message}");
                return false;
            }
        }

        public dynamic GetExtendedUserInfo(int userId)
        {
            // Dinamik sorgu metodunu kullan
            return GetUserDynamicById(userId);
        }

        #endregion

        #region Dinamik Kullanıcı Sorgulama Metotları

        public dynamic GetUserDynamicById(int userId)
        {
            return GetUserDynamicByFilter(u => u.Id == userId);
        }

        public dynamic GetUserDynamicByEmail(string email)
        {
            return GetUserDynamicByFilter(u => u.Email == email);
        }

        public dynamic GetUserDynamicByLogin(string login)
        {
            // Bu metot, ExtendedUser'daki LVUserLogin alanına göre sorgu yapar
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Users WHERE LVUserLogin = @Login", connection))
                {
                    command.Parameters.AddWithValue("@Login", login);

                    return ReadSingleUserDynamic(command);
                }
            }
        }

        public dynamic GetUserDynamicByFilter(Expression<Func<User, bool>> filter)
        {
            // Önce LINQ ile User nesnesini bul
            using (var context = new AuthDbContext(_connectionString))
            {
                var user = context.Users.AsNoTracking().FirstOrDefault(filter);
                if (user == null)
                    return null;

                // Dinamik nesne oluştur
                var dynamicUser = new ExpandoObject() as IDictionary<string, object>;

                // Temel User özelliklerini kopyala
                foreach (var property in typeof(User).GetProperties())
                {
                    dynamicUser[property.Name] = property.GetValue(user);
                }

                // Genişletilmiş özellikleri SQL ile al
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand($"SELECT * FROM Users WHERE Id = @UserId", connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user.Id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Genişletilmiş özellikleri al
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    if (!typeof(User).GetProperties().Any(p => p.Name == columnName))
                                    {
                                        dynamicUser[columnName] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    }
                                }
                            }
                        }
                    }
                }

                return dynamicUser;
            }
        }

        public List<dynamic> GetUsersDynamicByFilter(Expression<Func<User, bool>> filter)
        {
            // Önce LINQ ile User nesnelerini bul
            using (var context = new AuthDbContext(_connectionString))
            {
                var users = context.Users.AsNoTracking().Where(filter).ToList();

                if (users == null || users.Count == 0)
                    return new List<dynamic>();

                // Bulunan kullanıcıların ID'lerine göre tüm alanları SQL ile çek
                var result = new List<dynamic>();
                foreach (var user in users)
                {
                    var dynamicUser = GetUserDynamicWithAllFieldsById(user.Id);
                    if (dynamicUser != null)
                    {
                        result.Add(dynamicUser);
                    }
                }

                return result;
            }
        }

        private dynamic GetUserDynamicWithAllFieldsById(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Users WHERE Id = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    return ReadSingleUserDynamic(command);
                }
            }
        }

        private dynamic ReadSingleUserDynamic(SqlCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    dynamic dynamicUser = new ExpandoObject();
                    var expandoDict = (IDictionary<string, object>)dynamicUser;

                    // Tüm sütunları oku
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object value = reader.IsDBNull(i) ? null : reader.GetValue(i);

                        // byte[] tipindeki değerleri Base64 string'e dönüştür
                        if (value is byte[] byteArray)
                        {
                            value = Convert.ToBase64String(byteArray);
                        }

                        expandoDict[columnName] = value;
                    }

                    return dynamicUser;
                }

                return null;
            }
        }

        #endregion
    }
}