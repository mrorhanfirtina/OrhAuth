using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using System.Collections.Generic;

namespace OrhAuth.Services
{
    public interface IAuthService
    {
        // Mevcut metodlar
        User Register(UserForRegisterDto userForRegisterDto);
        User Login(UserForLoginDto userForLoginDto);
        bool UserExists(string email);
        AccessToken CreateAccessToken(User user);
        List<OperationClaim> GetClaims(User user);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        // TOKEN YÖNETİMİ
        AccessToken RefreshToken(string refreshToken);
        bool ValidateToken(string token);
        bool RevokeToken(string refreshToken);

        // KULLANICI YÖNETİMİ
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        bool SetUserStatus(int userId, bool isActive);
        List<User> GetUsers(int pageNumber, int pageSize);
        int GetUserCount();
        bool ChangePassword(int userId, string oldPassword, string newPassword);
        string RequestPasswordReset(string email);
        bool ResetPassword(string resetToken, string newPassword);

        // ROL VE YETKİ YÖNETİMİ
        OperationClaim CreateRole(string roleName);
        bool DeleteRole(int roleId);
        List<OperationClaim> GetRoles();
        bool AddClaim(int userId, string claimName);
        bool AssignRoleToUser(int userId, int roleId);
        bool RemoveRoleFromUser(int userId, int roleId);
        List<OperationClaim> GetUserRoles(int userId);
        bool HasPermission(int userId, string operationName);
        OperationClaim AddOperationClaim(string operationName);
        bool AssignClaimToRole(int roleId, int operationClaimId);

        // GENİŞLETİLMİŞ KULLANICI İŞLEMLERİ
        User RegisterExtendedUser(UserForRegisterDto baseUser, object extendedProperties);
        bool UpdateExtendedUser(int userId, object extendedProperties);
        dynamic GetExtendedUserInfo(int userId);
    }
}
