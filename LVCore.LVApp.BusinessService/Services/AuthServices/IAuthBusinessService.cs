using LVCore.LVApp.Shared.Dtos.AuthDtos;
using LVCore.LVApp.Shared.ExtendEntities;
using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LVCore.LVApp.BusinessService.Services.AuthServices
{
    public interface IAuthBusinessService
    {
        // 1. Kullanıcı Yönetimi
        Task<User> RegisterAsync(UserForRegisterDto model);
        Task<ExtendedUser> RegisterExtendedAsync(ExtendedUserForRegisterDto model);
        Task<AccessToken> LoginAsync(UserForLoginDto model);
        Task<bool> ActivateAccountAsync(string activationCode);
        Task<User> GetUserAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(UserUpdateDto model);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<bool> RequestPasswordResetAsync(string email);
        Task<bool> ResetPasswordAsync(string resetToken, string newPassword);

        // 2. Token Yönetimi
        Task<AccessToken> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string refreshToken);

        // 3. Rol ve Yetki Yönetimi
        Task<OperationClaim> AddRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<List<OperationClaim>> GetRolesAsync();
        Task<bool> AssignRoleToUserAsync(int userId, int roleId);
        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<List<OperationClaim>> GetUserRolesAsync(int userId);

        // 4. İzin ve Yetkilendirme
        Task<bool> HasPermissionAsync(int userId, string operationName);
        Task<OperationClaim> AddOperationClaimAsync(string operationName);
        Task<bool> AssignClaimToRoleAsync(int roleId, int operationClaimId);

        // 5. Genişletilmiş Özellikler (Extended User)
        Task<bool> UpdateExtendedUserFieldsAsync(ExtendedUserUpdateDto model);
        Task<ExtendedUser> GetExtendedUserInfoAsync(int userId);

        // 6. Yönetimsel İşlemler
        Task<bool> SetUserStatusAsync(int userId, bool isActive);
        Task<List<User>> GetUsersAsync(int pageNumber, int pageSize);
        Task<int> GetUserCountAsync();
        Task<bool> DeleteUserAsync(int userId);
    }
}
