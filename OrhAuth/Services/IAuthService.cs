using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrhAuth.Services
{
    /// <summary>
    /// Defines authentication, authorization, user, role, and token management operations for OrhAuth.
    /// </summary>
    public interface IAuthService
    {
        #region Registration & Authentication

        /// <summary>
        /// Registers a new user and assigns default roles.
        /// </summary>
        User Register(UserForRegisterDto userForRegisterDto);

        /// <summary>
        /// Authenticates a user using their credentials.
        /// </summary>
        User Login(UserForLoginDto userForLoginDto);

        /// <summary>
        /// Checks if a user with the specified email already exists.
        /// </summary>
        bool UserExists(string email);

        /// <summary>
        /// Creates an access token for a given user.
        /// </summary>
        AccessToken CreateAccessToken(User user);

        /// <summary>
        /// Creates an access token with additional custom claims.
        /// </summary>
        AccessToken CreateAccessToken(User user, Dictionary<string, string> customClaims = null);

        /// <summary>
        /// Retrieves the list of operation claims assigned to a user.
        /// </summary>
        List<OperationClaim> GetClaims(User user);

        /// <summary>
        /// Generates password hash and salt using HMACSHA512.
        /// </summary>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        #endregion

        #region Token Management

        /// <summary>
        /// Generates a new access token using the refresh token.
        /// </summary>
        AccessToken RefreshToken(string refreshToken);

        /// <summary>
        /// Generates a new access token using refresh token and custom claims.
        /// </summary>
        AccessToken RefreshToken(string refreshToken, Dictionary<string, string> customClaims = null);

        /// <summary>
        /// Retrieves user ID associated with the given refresh token.
        /// </summary>
        int GetUserIdByRefreshToken(string refreshToken);

        /// <summary>
        /// Validates the provided JWT token.
        /// </summary>
        bool ValidateToken(string token);

        /// <summary>
        /// Revokes the given refresh token.
        /// </summary>
        bool RevokeToken(string refreshToken);

        #endregion

        #region User Management

        /// <summary>
        /// Retrieves user by ID.
        /// </summary>
        User GetUserById(int userId);

        /// <summary>
        /// Retrieves user by email.
        /// </summary>
        User GetUserByEmail(string email);

        /// <summary>
        /// Updates user information.
        /// </summary>
        bool UpdateUser(User user);

        /// <summary>
        /// Marks the user as deleted.
        /// </summary>
        bool DeleteUser(int userId);

        /// <summary>
        /// Sets the user's active status.
        /// </summary>
        bool SetUserStatus(int userId, bool isActive);

        /// <summary>
        /// Retrieves a paginated list of users.
        /// </summary>
        List<User> GetUsers(int pageNumber, int pageSize);

        /// <summary>
        /// Returns the total number of users.
        /// </summary>
        int GetUserCount();

        /// <summary>
        /// Changes the password for a user after verifying the current one.
        /// </summary>
        bool ChangePassword(int userId, string oldPassword, string newPassword);

        /// <summary>
        /// Sends a password reset token to the specified user's email.
        /// </summary>
        string RequestPasswordReset(string email);

        /// <summary>
        /// Resets the user's password using a valid reset token.
        /// </summary>
        bool ResetPassword(string resetToken, string newPassword);

        #endregion

        #region Role & Claim Management

        /// <summary>
        /// Creates a new operation claim (role).
        /// </summary>
        OperationClaim CreateRole(string roleName);

        /// <summary>
        /// Deletes an existing operation claim.
        /// </summary>
        bool DeleteRole(int roleId);

        /// <summary>
        /// Retrieves all operation claims in the system.
        /// </summary>
        List<OperationClaim> GetRoles();

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        bool AddClaim(int userId, string claimName);

        /// <summary>
        /// Assigns an operation claim to a user.
        /// </summary>
        bool AssignRoleToUser(int userId, int roleId);

        /// <summary>
        /// Removes an assigned operation claim from a user.
        /// </summary>
        bool RemoveRoleFromUser(int userId, int roleId);

        /// <summary>
        /// Gets the list of roles assigned to a user.
        /// </summary>
        List<OperationClaim> GetUserRoles(int userId);

        /// <summary>
        /// Checks if a user has the specified permission.
        /// </summary>
        bool HasPermission(int userId, string operationName);

        /// <summary>
        /// Adds a new operation claim.
        /// </summary>
        OperationClaim AddOperationClaim(string operationName);

        /// <summary>
        /// Assigns a claim to a role (not implemented by default).
        /// </summary>
        bool AssignClaimToRole(int roleId, int operationClaimId);

        #endregion

        #region Extended User Methods

        /// <summary>
        /// Registers a new user with additional extended user properties.
        /// </summary>
        dynamic RegisterExtendedUser(UserForRegisterDto baseUser, object extendedProperties);

        /// <summary>
        /// Updates extended user fields.
        /// </summary>
        bool UpdateExtendedUser(int userId, object extendedProperties);

        /// <summary>
        /// Retrieves the extended user object with all dynamic fields.
        /// </summary>
        dynamic GetExtendedUserInfo(int userId);

        #endregion

        #region Dynamic User Fetching

        /// <summary>
        /// Retrieves a user as a dynamic object by ID.
        /// </summary>
        dynamic GetUserDynamicById(int userId);

        /// <summary>
        /// Retrieves a user as a dynamic object by email.
        /// </summary>
        dynamic GetUserDynamicByEmail(string email);

        /// <summary>
        /// Retrieves a user as a dynamic object by login identifier (username/email).
        /// </summary>
        dynamic GetUserDynamicByLogin(string login);

        /// <summary>
        /// Retrieves a single user as a dynamic object using a LINQ filter expression.
        /// </summary>
        dynamic GetUserDynamicByFilter(Expression<Func<User, bool>> filter);

        /// <summary>
        /// Retrieves a list of users as dynamic objects using a LINQ filter expression.
        /// </summary>
        List<dynamic> GetUsersDynamicByFilter(Expression<Func<User, bool>> filter);

        #endregion
    }
}
