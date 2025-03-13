using LVCore.LVApp.BusinessService.Services.AuthServices;
using LVCore.LVApp.Shared.Dtos.AuthDtos;
using LVCore.LVApp.Shared.ExtendEntities;
using LVCore.LVApp.Shared.Static;
using Microsoft.Owin.Logging;
using OrhAuth.Attributes;
using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using OrhAuth.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace LVCore.LVApp.BusinessService.Managers.AuthManagers
{
    public class AuthBusinessManager : IAuthBusinessService
    {
        private readonly IAuthService _authService;
        private readonly Services.ILoginBusinessService _loginBusinessManager;

        public AuthBusinessManager(IAuthService authService, Services.ILoginBusinessService loginBusinessManager)
        {
            _authService = authService;
            _loginBusinessManager = loginBusinessManager;
        }

        #region 1. Kullanıcı Yönetimi

        public async Task<User> RegisterAsync(UserForRegisterDto model)
        {
            return await Task.FromResult(_authService.Register(model));
        }

        public async Task<ExtendedUser> RegisterExtendedAsync(ExtendedUserForRegisterDto model)
        {
            // Auth servisinde RegisterExtendedUser metodunu kullan
            var baseUserDto = new UserForRegisterDto
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                LocalityId = model.LocalityId
            };

            baseUserDto.UserOperationClaims = model.UserOperationClaims;

            var user = await _loginBusinessManager.Login(model.LVUserLogin, model.LVPasswordText);

            // ExtendedUser modeli oluştur
            var extendedProperties = new ExtendedUser
            {
                // Özel alanları model'den al
                LVUserId = user.usr_ID,
                LVUserLogin = user.usr_Login,
                LVPassword = user.usr_Password,
                // Diğer extended özellikler...
            };

            var result = await Task.FromResult(_authService.RegisterExtendedUser(baseUserDto, extendedProperties));

            var extendedUser = ((ExpandoObject)result).ConvertTo<ExtendedUser>();

            return extendedUser;
        }

        public async Task<AccessToken> LoginAsync(UserForLoginDto model)
        {
            try
            {
                // Kullanıcı giriş yap
                var user = _authService.Login(model);

                // Kullanıcının tüm verilerini dinamik olarak çek
                dynamic userDynamic = _authService.GetUserDynamicById(user.Id);

                if (userDynamic == null)
                {
                    throw new Exception("Kullanıcı verileri alınamadı");
                }

                // Özel claim'leri oluştur
                var customClaims = new Dictionary<string, string>();

                // ExtendedUser tipini al
                Type extendedUserType = typeof(ExtendedUser);

                // AddToClaim özniteliği ile işaretlenmiş özellikleri bul
                var propertiesWithAddToClaim = extendedUserType.GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(AddToClaimAttribute), false).Length > 0)
                    .ToList();

                // Her bir özellik için
                foreach (var property in propertiesWithAddToClaim)
                {
                    // Özellik adını al
                    string propertyName = property.Name;

                    // AddToClaim özniteliğini al
                    var addToClaimAttr = property.GetCustomAttributes(typeof(AddToClaimAttribute), false)
                        .FirstOrDefault() as AddToClaimAttribute;

                    // Dynamic nesneden değeri al
                    object value = null;

                    // Dynamic nesne üzerinde property var mı kontrol et
                    bool propertyExists = false;
                    foreach (KeyValuePair<string, object> item in userDynamic)
                    {
                        if (item.Key == propertyName)
                        {
                            propertyExists = true;
                            value = item.Value;
                            break;
                        }
                    }

                    // Eğer özellik dynamic nesnede varsa ve değeri null değilse
                    if (propertyExists && value != null)
                    {
                        // Claim adını belirle (öznitelikte belirtilmişse onu kullan, yoksa özellik adını)
                        string claimName = !string.IsNullOrEmpty(addToClaimAttr.ClaimName)
                            ? addToClaimAttr.ClaimName
                            : propertyName;

                        // Prefix ekle
                        string fullClaimName = addToClaimAttr.Prefix + claimName;

                        // Dictionary'e ekle
                        customClaims[fullClaimName] = value.ToString();
                    }
                }

                // Token oluştur
                AccessToken token;

                // Eğer özel claim'ler varsa, bunları kullanarak token oluştur
                if (customClaims.Count > 0)
                {
                    token = _authService.CreateAccessToken(user, customClaims);
                }
                else
                {
                    // Özel claim yoksa, standart token oluştur
                    token = _authService.CreateAccessToken(user);
                }

                return await Task.FromResult(token);
            }
            catch (Exception ex)
            {
                throw new Exception("LoginAsync sırasında hata oluştu");
            }
        }



        public async Task<bool> ActivateAccountAsync(string activationCode)
        {
            // Aktivasyon kodunu doğrulama ve kullanıcı hesabını aktif etme işlemi
            // Bu metodun implementasyonu auth serviste yok, eklenmesi gerekebilir
            // Şimdilik örnek bir implementasyon:

            try
            {
                // Aktivasyon kodunu kullanarak kullanıcıyı bul
                // Bu kısım gerçek uygulamada implementasyona göre değişecektir

                // Kullanıcıyı aktifleştir
                // var user = _authService.GetUserByActivationCode(activationCode);
                // if (user != null)
                // {
                //     user.IsActive = true;
                //     user.ActivationCode = null;
                //     return await Task.FromResult(_authService.UpdateUser(user));
                // }

                return await Task.FromResult(false);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await Task.FromResult(_authService.GetUserById(userId));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await Task.FromResult(_authService.GetUserByEmail(email));
        }

        public async Task<bool> UpdateUserAsync(UserUpdateDto model)
        {
            var user = _authService.GetUserById(model.UserId);
            if (user == null)
                return await Task.FromResult(false);

            // Kullanıcı bilgilerini güncelle
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            // Diğer alanları güncelle

            return await Task.FromResult(_authService.UpdateUser(user));
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            return await Task.FromResult(_authService.ChangePassword(userId, oldPassword, newPassword));
        }

        public async Task<bool> RequestPasswordResetAsync(string email)
        {
            // Şifre sıfırlama isteği oluştur
            string resetToken = _authService.RequestPasswordReset(email);
            return await Task.FromResult(!string.IsNullOrEmpty(resetToken));
        }

        public async Task<bool> ResetPasswordAsync(string resetToken, string newPassword)
        {
            return await Task.FromResult(_authService.ResetPassword(resetToken, newPassword));
        }

        #endregion

        #region 2. Token Yönetimi

        public async Task<AccessToken> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Önce refresh token'ı doğrula ve kullanıcı ID'sini al
                var userId = _authService.GetUserIdByRefreshToken(refreshToken);
                if (!(userId > 0))
                {
                    throw new Exception("Geçersiz refresh token");
                }

                // Kullanıcının tüm verilerini dinamik olarak çek
                dynamic userDynamic = _authService.GetUserDynamicById(userId);

                if (userDynamic == null)
                {
                    throw new Exception("Kullanıcı verileri alınamadı");
                }

                // Özel claim'leri oluştur
                var customClaims = new Dictionary<string, string>();

                // ExtendedUser tipini al
                Type extendedUserType = typeof(ExtendedUser);

                // AddToClaim özniteliği ile işaretlenmiş özellikleri bul
                var propertiesWithAddToClaim = extendedUserType.GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(AddToClaimAttribute), false).Length > 0)
                    .ToList();

                // Her bir özellik için
                foreach (var property in propertiesWithAddToClaim)
                {
                    // Özellik adını al
                    string propertyName = property.Name;

                    // AddToClaim özniteliğini al
                    var addToClaimAttr = property.GetCustomAttributes(typeof(AddToClaimAttribute), false)
                        .FirstOrDefault() as AddToClaimAttribute;

                    // Dynamic nesneden değeri al
                    object value = null;

                    // Dynamic nesne üzerinde property var mı kontrol et
                    bool propertyExists = false;
                    foreach (KeyValuePair<string, object> item in userDynamic)
                    {
                        if (item.Key == propertyName)
                        {
                            propertyExists = true;
                            value = item.Value;
                            break;
                        }
                    }

                    // Eğer özellik dynamic nesnede varsa ve değeri null değilse
                    if (propertyExists && value != null)
                    {
                        // Claim adını belirle (öznitelikte belirtilmişse onu kullan, yoksa özellik adını)
                        string claimName = !string.IsNullOrEmpty(addToClaimAttr.ClaimName)
                            ? addToClaimAttr.ClaimName
                            : propertyName;

                        // Prefix ekle
                        string fullClaimName = addToClaimAttr.Prefix + claimName;

                        // Dictionary'e ekle
                        customClaims[fullClaimName] = value.ToString();
                    }
                }

                // Token oluştur
                AccessToken token;

                // Eğer özel claim'ler varsa, bunları kullanarak token oluştur
                if (customClaims.Count > 0)
                {
                    token = _authService.RefreshToken(refreshToken, customClaims);
                }
                else
                {
                    // Özel claim yoksa, standart token oluştur
                    token = _authService.RefreshToken(refreshToken);
                }

                return await Task.FromResult(token);
            }
            catch (Exception ex)
            {
                // Hata loglama
                throw new Exception("RefreshTokenAsync sırasında hata oluştu");
            }
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            return await Task.FromResult(_authService.ValidateToken(token));
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            return await Task.FromResult(_authService.RevokeToken(refreshToken));
        }

        #endregion

        #region 3. Rol ve Yetki Yönetimi

        public async Task<OperationClaim> AddRoleAsync(string roleName)
        {
            return await Task.FromResult(_authService.CreateRole(roleName));
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            return await Task.FromResult(_authService.DeleteRole(roleId));
        }

        public async Task<List<OperationClaim>> GetRolesAsync()
        {
            return await Task.FromResult(_authService.GetRoles());
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            return await Task.FromResult(_authService.AssignRoleToUser(userId, roleId));
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            return await Task.FromResult(_authService.RemoveRoleFromUser(userId, roleId));
        }

        public async Task<List<OperationClaim>> GetUserRolesAsync(int userId)
        {
            return await Task.FromResult(_authService.GetUserRoles(userId));
        }

        #endregion

        #region 4. İzin ve Yetkilendirme

        public async Task<bool> HasPermissionAsync(int userId, string operationName)
        {
            return await Task.FromResult(_authService.HasPermission(userId, operationName));
        }

        public async Task<OperationClaim> AddOperationClaimAsync(string operationName)
        {
            return await Task.FromResult(_authService.AddOperationClaim(operationName));
        }

        public async Task<bool> AssignClaimToRoleAsync(int roleId, int operationClaimId)
        {
            return await Task.FromResult(_authService.AssignClaimToRole(roleId, operationClaimId));
        }

        #endregion

        #region 5. Genişletilmiş Özellikler (Extended User)

        public async Task<bool> UpdateExtendedUserFieldsAsync(ExtendedUserUpdateDto model)
        {
            try
            {
                // Önce kullanıcıyı al
                var user = _authService.GetUserById(model.UserId);
                if (user == null)
                    return await Task.FromResult(false);

                // Eğer user zaten ExtendedUser türündeyse, extended alanlarını güncelle
                if (user is ExtendedUser extendedUser)
                {
                    extendedUser.LVUserId = model.LVUserId;
                    extendedUser.LVUserLogin = model.LVUserLogin;
                    extendedUser.LVPassword = model.LVPasswordText;
                    // Diğer genişletilmiş alanları burada güncelle

                    return await Task.FromResult(_authService.UpdateUser(extendedUser));
                }

                return await Task.FromResult(false);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<ExtendedUser> GetExtendedUserInfoAsync(int userId)
        {
            var user = _authService.GetUserById(userId);
            return await Task.FromResult(user as ExtendedUser);
        }

        #endregion

        #region 6. Yönetimsel İşlemler

        public async Task<bool> SetUserStatusAsync(int userId, bool isActive)
        {
            return await Task.FromResult(_authService.SetUserStatus(userId, isActive));
        }

        public async Task<List<User>> GetUsersAsync(int pageNumber, int pageSize)
        {
            return await Task.FromResult(_authService.GetUsers(pageNumber, pageSize));
        }

        public async Task<int> GetUserCountAsync()
        {
            return await Task.FromResult(_authService.GetUserCount());
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await Task.FromResult(_authService.DeleteUser(userId));
        }

        #endregion
    }
}
