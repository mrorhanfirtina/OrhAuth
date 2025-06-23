using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Abstract;
using OrhAuth.Exceptions;
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
    /// <summary>
    /// Provides core implementation for authentication, authorization, token handling, 
    /// user and role management operations in the OrhAuth framework.
    /// </summary>
    /// <remarks>
    /// This class serves as the main entry point for managing user registration, login, 
    /// JWT access token creation and validation, role assignments, 
    /// refresh token operations, and extended user support.
    /// </remarks>
    public class AuthManager : IAuthService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<OperationClaim> _operationClaimRepository;
        private readonly IEntityRepository<UserOperationClaim> _userOperationClaimRepository;
        private readonly IEntityRepository<RefreshToken> _refreshTokenRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly string _connectionString;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthManager"/> class with the required repositories, token helper, and database connection string.
        /// </summary>
        /// <param name="userRepository">Repository for accessing and managing <see cref="User"/> entities.</param>
        /// <param name="operationClaimRepository">Repository for accessing and managing <see cref="OperationClaim"/> entities.</param>
        /// <param name="userOperationClaimRepository">Repository for managing user-role relationships.</param>
        /// <param name="refreshTokenRepository">Repository for handling refresh token operations.</param>
        /// <param name="tokenHelper">Service for creating and validating JWT access tokens.</param>
        /// <param name="connectionString">Database connection string used for creating new data contexts.</param>
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

        #region Common Methods

        /// <summary>
        /// Registers a new user by hashing the password and storing user details along with assigned roles.
        /// </summary>
        /// <param name="userForRegisterDto">The DTO containing user registration information including email, name, password, and optional role claims.</param>
        /// <returns>The newly created <see cref="User"/> entity.</returns>
        /// <remarks>
        /// - Password is hashed and salted before storing.
        /// - The user is marked as active by default.
        /// - Any roles included in the DTO are added to the user's claims.
        /// </remarks>
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

            foreach (var claim in userForRegisterDto.UserOperationClaims)
            {
                user.UserOperationClaims.Add(new UserOperationClaim { OperationClaimId = claim.OperationClaimId });
            }

            _userRepository.Add(user);
            return user;
        }

        /// <summary>
        /// Authenticates a user based on the provided email and password.
        /// </summary>
        /// <param name="userForLoginDto">DTO containing the user's email and password.</param>
        /// <returns>The authenticated <see cref="User"/> entity if credentials are valid.</returns>
        /// <exception cref="OrhAuthException">Thrown when the user is not found or the password is incorrect.</exception>
        /// <remarks>
        /// - Retrieves the user by email.
        /// - Verifies the provided password against the stored hash and salt.
        /// </remarks>
        public User Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userRepository.Get(u => u.Email == userForLoginDto.Email);
            if (userToCheck == null)
                throw new OrhAuthException("User not found.");


            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                throw new OrhAuthException("Password error.");

            return userToCheck;
        }

        /// <summary>
        /// Checks whether a user with the specified email address already exists in the system.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns><c>true</c> if a user with the given email exists; otherwise, <c>false</c>.</returns>
        public bool UserExists(string email)
        {
            return _userRepository.Get(u => u.Email == email) != null;
        }

        /// <summary>
        /// Generates a new access token and refresh token for the specified user,
        /// saves the refresh token to the database, and returns the access token.
        /// </summary>
        /// <param name="user">The user for whom the access token will be created.</param>
        /// <returns>
        /// An <see cref="AccessToken"/> object containing the JWT, refresh token, and expiration information.
        /// </returns>
        /// <remarks>
        /// The refresh token is saved to the database for future token renewal operations.
        /// Note: The client's actual IP address should be used instead of a hardcoded value.
        /// </remarks>
        public AccessToken CreateAccessToken(User user, string createdByIp = "127.0.0.1")
        {
            var claims = GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = accessToken.RefreshToken,
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = createdByIp // Gerçek projede istemcinin IP'sini almalısınız!!!
            };
            _refreshTokenRepository.Add(refreshToken);

            return accessToken;
        }

        /// <summary>
        /// Generates a new access token and refresh token for the specified user,
        /// including custom claims if provided. Saves the refresh token to the database.
        /// </summary>
        /// <param name="user">The user for whom the token will be generated.</param>
        /// <param name="customClaims">
        /// A dictionary of additional claims to include in the token. 
        /// If null or empty, only standard claims will be included.
        /// </param>
        /// <returns>
        /// An <see cref="AccessToken"/> object that contains the JWT, refresh token, and expiration information.
        /// </returns>
        /// <remarks>
        /// Custom claims can be used to include additional contextual information such as department, role metadata, or location.
        /// Note: You should use the actual client IP address instead of the hardcoded "127.0.0.1".
        /// </remarks>
        public AccessToken CreateAccessToken(User user, Dictionary<string, string> customClaims = null, string createdByIp = "127.0.0.1")
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
                CreatedByIp = createdByIp // Gerçek projede istemcinin IP'sini almalısınız
            };

            // Refresh token'ı kaydet
            _refreshTokenRepository.Add(refreshToken);

            return accessToken;
        }

        /// <summary>
        /// Retrieves the list of <see cref="OperationClaim"/>s (roles/permissions) assigned to the specified user.
        /// </summary>
        /// <param name="user">The user whose claims will be fetched.</param>
        /// <returns>
        /// A list of <see cref="OperationClaim"/> objects representing the user's roles or permissions.
        /// </returns>
        /// <remarks>
        /// This method performs individual lookups for each operation claim based on the user's assigned claim mappings.
        /// </remarks>
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

        /// <summary>
        /// Generates a password hash and salt using the provided plain text password.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <param name="passwordHash">The resulting password hash.</param>
        /// <param name="passwordSalt">The randomly generated salt used in hashing.</param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        }

        /// <summary>
        /// Verifies whether the provided plain text password matches the stored hash and salt.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="passwordHash">The stored password hash.</param>
        /// <param name="passwordSalt">The stored password salt.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        }

        #endregion

        #region Token Management

        /// <summary>
        /// Refreshes an expired access token using a valid refresh token. 
        /// Revokes the old refresh token and issues a new one.
        /// </summary>
        /// <param name="refreshToken">The refresh token provided by the client.</param>
        /// <returns>An <see cref="AccessToken"/> containing a new JWT and refresh token.</returns>
        /// <exception cref="OrhAuthException">
        /// Thrown when the refresh token is invalid, expired, or the associated user is not found or inactive.
        /// </exception>
        public AccessToken RefreshToken(string refreshToken, string createdByIp = "127.0.0.1")
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken && rt.RevokedDate == null);
            if (token == null)
                throw new OrhAuthException("Invalid refresh token.");

            // Token süresi dolmuş mu?
            if (token.Expires < DateTime.Now)
            {
                // Refresh token'ı iptal et
                token.RevokedDate = DateTime.Now;
                token.Revoked = DateTime.Now;
                _refreshTokenRepository.Update(token);
                throw new OrhAuthException("The refresh token has expired.");
            }

            var user = _userRepository.Get(u => u.Id == token.UserId);
            if (user == null || !user.IsActive)
                throw new OrhAuthException("User not found or is inactive.");

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
                CreatedByIp = createdByIp // Gerçek projede istemcinin IP'sini almalısınız
            };
            _refreshTokenRepository.Add(newRefreshToken);

            return accessToken;
        }

        /// <summary>
        /// Refreshes an expired access token using a valid refresh token, 
        /// optionally adding custom claims to the newly issued token.
        /// </summary>
        /// <param name="refreshToken">The refresh token provided by the client.</param>
        /// <param name="customClaims">
        /// Optional custom claims to include in the new JWT. 
        /// These can be used to inject additional metadata into the token.
        /// </param>
        /// <returns>
        /// A new <see cref="AccessToken"/> instance containing a fresh JWT and refresh token.
        /// </returns>
        /// <exception cref="OrhAuthException">
        /// Thrown when the refresh token is invalid, expired, or the associated user is not found or inactive.
        /// </exception>
        public AccessToken RefreshToken(string refreshToken, Dictionary<string, string> customClaims = null, string createdByIp = "127.0.0.1")
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken && rt.RevokedDate == null);
            if (token == null)
                throw new OrhAuthException("Invalid refresh token.");

            if (token.Expires < DateTime.Now)
            {
                token.RevokedDate = DateTime.Now;
                token.Revoked = DateTime.Now;
                _refreshTokenRepository.Update(token);
                throw new OrhAuthException("Refresh token expired.");
            }

            var user = _userRepository.Get(u => u.Id == token.UserId);
            if (user == null || !user.IsActive)
                throw new OrhAuthException("User not found or is inactive.");

            var claims = GetClaims(user);

            AccessToken accessToken;

            if (customClaims != null && customClaims.Count > 0)
            {
                accessToken = _tokenHelper.CreateToken(user, claims, customClaims);
            }
            else
            {
                accessToken = _tokenHelper.CreateToken(user, claims);
            }

            token.RevokedDate = DateTime.Now;
            token.Revoked = DateTime.Now;
            token.ReplacedByToken = accessToken.RefreshToken;
            _refreshTokenRepository.Update(token);

            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = accessToken.RefreshToken,
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = createdByIp // Gerçek projede istemcinin IP'sini almalısınız
            };
            _refreshTokenRepository.Add(newRefreshToken);

            return accessToken;
        }

        /// <summary>
        /// Retrieves the user ID associated with a valid, non-revoked refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to validate and query.</param>
        /// <returns>
        /// The user ID if the refresh token is valid and not expired; 
        /// otherwise returns <c>0</c>.
        /// </returns>
        /// <remarks>
        /// If the refresh token is expired, it will be marked as revoked before returning <c>0</c>.
        /// </remarks>
        public int GetUserIdByRefreshToken(string refreshToken)
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken && rt.RevokedDate == null);
            if (token == null)
                return 0;

            if (token.Expires < DateTime.Now)
            {
                token.RevokedDate = DateTime.Now;
                _refreshTokenRepository.Update(token);
                return 0;
            }

            return token.UserId;
        }

        /// <summary>
        /// Validates the given JWT access token using the token helper.
        /// </summary>
        /// <param name="token">The JWT token string to validate.</param>
        /// <returns>
        /// <c>true</c> if the token is valid and has not expired or been tampered with; 
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Any exceptions thrown by the token validation logic are caught and suppressed.
        /// </remarks>
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

        /// <summary>
        /// Revokes the specified refresh token by setting its <c>RevokedDate</c>.
        /// </summary>
        /// <param name="refreshToken">The refresh token string to revoke.</param>
        /// <returns>
        /// <c>true</c> if the token was found and successfully revoked; 
        /// <c>false</c> if the token was not found.
        /// </returns>
        /// <remarks>
        /// This method marks the refresh token as revoked without physically deleting it from the database.
        /// </remarks>
        public bool RevokeToken(string refreshToken)
        {
            var token = _refreshTokenRepository.Get(rt => rt.Token == refreshToken);
            if (token == null)
                return false;

            token.RevokedDate = DateTime.Now;
            _refreshTokenRepository.Update(token);
            return true;
        }

        #endregion

        #region User Management

        /// <summary>
        /// Retrieves a user entity by its unique identifier.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> entity if found; otherwise, <c>null</c>.</returns>
        public User GetUserById(int userId)
        {
            return _userRepository.Get(u => u.Id == userId);
        }

        /// <summary>
        /// Retrieves a user entity by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> entity if found; otherwise, <c>null</c>.</returns>
        public User GetUserByEmail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }

        /// <summary>
        /// Updates the basic information of an existing user.
        /// </summary>
        /// <param name="user">The user entity containing updated data.</param>
        /// <returns>
        /// <c>true</c> if the update was successful; 
        /// <c>false</c> if the user could not be found.
        /// </returns>
        public bool UpdateUser(User user)
        {
            var existingUser = _userRepository.Get(u => u.Id == user.Id);
            if (existingUser == null)
                return false;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.LocalityId = user.LocalityId;

            _userRepository.Update(existingUser);
            return true;
        }

        /// <summary>
        /// Deletes a user by marking them as deleted and revokes related tokens and claims.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>
        /// <c>true</c> if the user was found and deleted; 
        /// <c>false</c> if the user does not exist.
        /// </returns>
        /// <remarks>
        /// This method performs a soft delete by marking the user as deleted.
        /// It also revokes all active refresh tokens and removes user-role associations.
        /// </remarks>
        public bool DeleteUser(int userId)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null)
                return false;

            var refreshTokens = _refreshTokenRepository.GetList(rt => rt.UserId == userId && rt.RevokedDate == null);
            foreach (var token in refreshTokens)
            {
                token.RevokedDate = DateTime.Now;
                _refreshTokenRepository.Update(token);
            }

            var userClaims = _userOperationClaimRepository.GetList(uoc => uoc.UserId == userId);
            foreach (var claim in userClaims)
            {
                _userOperationClaimRepository.Delete(claim);
            }

            _userRepository.Delete(user);
            return true;
        }

        /// <summary>
        /// Updates the active status of a user (enable or disable the user account).
        /// </summary>
        /// <param name="userId">The ID of the user whose status is to be changed.</param>
        /// <param name="isActive">Indicates whether the user should be active or inactive.</param>
        /// <returns>
        /// <c>true</c> if the user was found and updated; 
        /// <c>false</c> if the user does not exist.
        /// </returns>
        public bool SetUserStatus(int userId, bool isActive)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null)
                return false;

            user.IsActive = isActive;
            _userRepository.Update(user);
            return true;
        }

        /// <summary>
        /// Retrieves a paginated list of users.
        /// </summary>
        /// <param name="pageNumber">The page number (starting from 1).</param>
        /// <param name="pageSize">The number of users to include per page.</param>
        /// <returns>A list of users for the specified page.</returns>
        public List<User> GetUsers(int pageNumber, int pageSize)
        {
            return _userRepository.GetList()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Returns the total number of users in the system.
        /// </summary>
        /// <returns>The total user count.</returns>
        public int GetUserCount()
        {
            return _userRepository.GetList().Count;
        }

        /// <summary>
        /// Changes the password of a user after verifying the current password.
        /// </summary>
        /// <param name="userId">The ID of the user whose password is to be changed.</param>
        /// <param name="oldPassword">The user's current password.</param>
        /// <param name="newPassword">The new password to be set.</param>
        /// <returns>True if the password was successfully changed; otherwise, false.</returns>
        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            if (user == null)
                return false;

            if (!HashingHelper.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
                return false;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Update(user);
            return true;
        }

        /// <summary>
        /// Generates a password reset token for the user and updates the user record with the token details.
        /// </summary>
        /// <param name="email">The email address of the user requesting a password reset.</param>
        /// <returns>
        /// The generated reset token if the user is found; otherwise, the string "false".
        /// </returns>
        public string RequestPasswordReset(string email)
        {
            var user = _userRepository.Get(u => u.Email == email);
            if (user == null)
                return "false";

            var resetToken = Guid.NewGuid().ToString("N");

            byte[] tokenHash, tokenSalt;
            HashingHelper.CreatePasswordHash(resetToken, out tokenHash, out tokenSalt);

            user.PasswordResetToken = Convert.ToBase64String(tokenHash);
            user.PasswordResetTokenSalt = Convert.ToBase64String(tokenSalt);
            user.PasswordResetTokenExpiry = DateTime.Now.AddHours(24);

            _userRepository.Update(user);

            return resetToken;
        }

        /// <summary>
        /// Resets the user's password by validating the provided reset token.
        /// If the token is valid and not expired, the user's password is updated.
        /// </summary>
        /// <param name="resetToken">The password reset token provided by the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>
        /// <c>true</c> if the password was successfully reset; otherwise, <c>false</c>.
        /// </returns>
        public bool ResetPassword(string resetToken, string newPassword)
        {
            var users = _userRepository.GetList().ToList();

            foreach (var user in users)
            {
                if (string.IsNullOrEmpty(user.PasswordResetToken) ||
                    user.PasswordResetTokenExpiry < DateTime.Now)
                    continue;

                byte[] tokenHash = Convert.FromBase64String(user.PasswordResetToken);
                byte[] tokenSalt = Convert.FromBase64String(user.PasswordResetTokenSalt);

                if (HashingHelper.VerifyPasswordHash(resetToken, tokenHash, tokenSalt))
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

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

        #region Role And Auth Management

        /// <summary>
        /// Creates a new role (operation claim) with the specified name if it does not already exist.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>
        /// The created <see cref="OperationClaim"/> object, or the existing one if a role with the same name already exists.
        /// </returns>
        public OperationClaim CreateRole(string roleName)
        {
            var existingRole = _operationClaimRepository.Get(oc => oc.Name == roleName);
            if (existingRole != null)
                return existingRole;

            var role = new OperationClaim { Name = roleName };
            _operationClaimRepository.Add(role);
            return role;
        }

        /// <summary>
        /// Deletes a role (operation claim) by its ID and removes all associated user-role relationships.
        /// </summary>
        /// <param name="roleId">The ID of the role to delete.</param>
        /// <returns>
        /// <c>true</c> if the role was found and deleted successfully; otherwise, <c>false</c>.
        /// </returns>
        public bool DeleteRole(int roleId)
        {
            var role = _operationClaimRepository.Get(oc => oc.Id == roleId);
            if (role == null)
                return false;

            var userRoles = _userOperationClaimRepository.GetList(uoc => uoc.OperationClaimId == roleId);
            foreach (var userRole in userRoles)
            {
                _userOperationClaimRepository.Delete(userRole);
            }

            _operationClaimRepository.Delete(role);
            return true;
        }

        /// <summary>
        /// Retrieves all roles (operation claims) defined in the system.
        /// </summary>
        /// <returns>A list of all <see cref="OperationClaim"/> objects.</returns>
        public List<OperationClaim> GetRoles()
        {
            return _operationClaimRepository.GetList().ToList();
        }

        /// <summary>
        /// Assigns a role (operation claim) to a user if it's not already assigned.
        /// </summary>
        /// <param name="userId">The ID of the user to assign the claim to.</param>
        /// <param name="claimName">The name of the claim to assign.</param>
        /// <returns>
        /// True if the claim is successfully assigned or already exists; 
        /// false if the user or claim is not found.
        /// </returns>
        public bool AddClaim(int userId, string claimName)
        {
            var user = _userRepository.Get(u => u.Id == userId);
            var claim = _operationClaimRepository.Get(oc => oc.Name == claimName);

            if (user == null || claim == null)
                return false;

            var existingClaim = _userOperationClaimRepository.Get(uoc =>
                uoc.UserId == userId && uoc.OperationClaimId == claim.Id);

            if (existingClaim != null)
                return true;

            var userOperationClaim = new UserOperationClaim
            {
                UserId = userId,
                OperationClaimId = claim.Id
            };
            _userOperationClaimRepository.Add(userOperationClaim);
            return true;
        }

        /// <summary>
        /// Assigns a specific role (operation claim) to the given user if not already assigned.
        /// </summary>
        /// <param name="userId">The ID of the user to whom the role will be assigned.</param>
        /// <param name="roleId">The ID of the role to assign.</param>
        /// <returns>
        /// True if the role is successfully assigned or already exists; 
        /// false if the user or role is not found.
        /// </returns>
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

        /// <summary>
        /// Removes the specified role (operation claim) from the given user.
        /// </summary>
        /// <param name="userId">The ID of the user from whom the role will be removed.</param>
        /// <param name="roleId">The ID of the role to be removed.</param>
        /// <returns>
        /// True if the role was successfully removed; 
        /// false if the user-role relationship was not found.
        /// </returns>
        public bool RemoveRoleFromUser(int userId, int roleId)
        {
            var userRole = _userOperationClaimRepository.Get(uoc =>
                uoc.UserId == userId && uoc.OperationClaimId == roleId);

            if (userRole == null)
                return false;

            _userOperationClaimRepository.Delete(userRole);
            return true;
        }

        /// <summary>
        /// Retrieves all roles (operation claims) assigned to the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose roles will be retrieved.</param>
        /// <returns>A list of <see cref="OperationClaim"/> objects assigned to the user.</returns>
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

        /// <summary>
        /// Checks whether the specified user has the given permission (operation claim).
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="operationName">The name of the operation/claim to check.</param>
        /// <returns>
        /// True if the user is active and has the specified claim; otherwise, false.
        /// </returns>
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

        /// <summary>
        /// Adds a new operation claim (permission) if it doesn't already exist.
        /// </summary>
        /// <param name="operationName">The name of the operation claim to add.</param>
        /// <returns>
        /// The newly created OperationClaim if it was not present, or the existing one if it already exists.
        /// </returns>
        public OperationClaim AddOperationClaim(string operationName)
        {
            var existingClaim = _operationClaimRepository.Get(oc => oc.Name == operationName);
            if (existingClaim != null)
                return existingClaim;

            var claim = new OperationClaim { Name = operationName };
            _operationClaimRepository.Add(claim);
            return claim;
        }

        /// <summary>
        /// Assigns an additional permission (operation claim) to all users who currently have the specified role.
        /// </summary>
        /// <param name="roleId">The ID of the role whose users will receive the new claim.</param>
        /// <param name="operationClaimId">The ID of the operation claim to assign.</param>
        /// <returns>
        /// True if the operation completes successfully, false if the role or claim does not exist or an error occurs.
        /// </returns>
        public bool AssignClaimToRole(int roleId, int operationClaimId)
        {
            try
            {
                var role = _operationClaimRepository.Get(r => r.Id == roleId);
                var claim = _operationClaimRepository.Get(c => c.Id == operationClaimId);

                if (role == null || claim == null)
                    return false;

                var usersInRole = _userOperationClaimRepository.GetList(uoc => uoc.OperationClaimId == roleId).ToList();

                foreach (var userRole in usersInRole)
                {
                    var hasPermission = _userOperationClaimRepository.Get(
                        uoc => uoc.UserId == userRole.UserId && uoc.OperationClaimId == operationClaimId);

                    if (hasPermission == null)
                    {
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

        #region Expanded User Operations

        /// <summary>
        /// Registers a new user using the standard registration process and then adds extended properties to the user dynamically.
        /// </summary>
        /// <param name="baseUser">DTO containing the base registration information (email, password, first/last name, etc.).</param>
        /// <param name="extendedProperties">
        /// An anonymous object or dynamic type containing additional extended user properties.
        /// These must match the extended type registered via <c>SchemaMetadataCache.RegisterExtendedType</c>.
        /// </param>
        /// <returns>
        /// A dynamic user object with both base and extended properties populated, or <c>null</c> if an error occurs.
        /// </returns>
        public dynamic RegisterExtendedUser(UserForRegisterDto baseUser, object extendedProperties)
        {
            try
            {
                var user = Register(baseUser);

                System.Diagnostics.Debug.WriteLine($"Is the user null?: {user == null}");
                System.Diagnostics.Debug.WriteLine($"Are the extended properties null?: {extendedProperties == null}");
                if (extendedProperties != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Type of extendedProperties: {extendedProperties.GetType().FullName}");
                }

                if (user == null)
                {
                    System.Diagnostics.Debug.WriteLine("User is null!");
                    return user;
                }

                if (extendedProperties == null)
                {
                    System.Diagnostics.Debug.WriteLine("Extended properties are null!");
                    return user;
                }

                UpdateExtendedPropertiesWithSQL(user.Id, extendedProperties);

                return GetUserDynamicById(user.Id);
            }
            catch (Exception ex)
            {
                throw new OrhAuthException($"RegisterExtendedUser error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates the extended properties of a registered user dynamically using SQL.
        /// </summary>
        /// <param name="userId">The ID of the user whose extended properties will be updated.</param>
        /// <param name="extendedProperties">
        /// An object containing extended user properties. These must match the structure of the extended user type
        /// previously registered using <c>SchemaMetadataCache.RegisterExtendedType</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the update operation succeeds; otherwise, <c>false</c>.
        /// </returns>
        public bool UpdateExtendedUser(int userId, object extendedProperties)
        {
            try
            {
                var user = _userRepository.Get(u => u.Id == userId);
                if (user == null || extendedProperties == null)
                    return false;

                return UpdateExtendedPropertiesWithSQL(userId, extendedProperties);
            }
            catch (Exception ex)
            {
                throw new OrhAuthException($"UpdateExtendedUser error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates extended properties of a user in the database using raw SQL.
        /// Only properties marked with the <see cref="Attributes.ExtendUserAttribute"/> are included in the update.
        /// </summary>
        /// <param name="userId">The ID of the user to be updated.</param>
        /// <param name="extendedProperties">
        /// An object containing extended user properties that match the defined extended user type.
        /// </param>
        /// <returns>
        /// <c>true</c> if the update was successful (i.e., one or more rows affected); otherwise, <c>false</c>.
        /// </returns>
        private bool UpdateExtendedPropertiesWithSQL(int userId, object extendedProperties)
        {
            try
            {
                var updateColumns = new Dictionary<string, object>();

                foreach (var property in extendedProperties.GetType().GetProperties())
                {
                    if (property.IsDefined(typeof(Attributes.ExtendUserAttribute), false))
                    {
                        var value = property.GetValue(extendedProperties);
                        updateColumns.Add(property.Name, value);
                    }
                }

                if (updateColumns.Count == 0)
                    return false;

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
                throw new OrhAuthException($"UpdateExtendedPropertiesWithSQL error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Converts a standard <see cref="User"/> object into an extended user type instance by copying all base properties.
        /// This is useful when registering or upgrading a user to a dynamically defined extended user class.
        /// </summary>
        /// <param name="user">The original <see cref="User"/> entity to be converted.</param>
        /// <param name="extendedUserType">The extended <see cref="Type"/> that inherits from <see cref="User"/>.</param>
        /// <returns>
        /// A new instance of the extended user type populated with the original user's data, or <c>null</c> if the conversion fails.
        /// </returns>
        private User ConvertToExtendedUser(User user, Type extendedUserType)
        {
            try
            {
                var extendedUser = Activator.CreateInstance(extendedUserType) as User;
                if (extendedUser == null)
                    return null;

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

                _userRepository.Delete(user);

                _userRepository.Add(extendedUser);

                return extendedUser;
            }
            catch (Exception ex)
            {
                throw new OrhAuthException($"ConvertToExtendedUser error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates the extended properties of a user instance by copying values from a provided extended object.
        /// This method maintains backward compatibility by directly updating properties within the <see cref="User"/> class.
        /// </summary>
        /// <param name="user">The <see cref="User"/> instance to be updated.</param>
        /// <param name="extendedProperties">An object containing properties marked with <see cref="Attributes.ExtendUserAttribute"/>.</param>
        /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
        private bool UpdateStandardExtendedUser(User user, object extendedProperties)
        {
            try
            {
                foreach (var property in extendedProperties.GetType().GetProperties())
                {
                    if (property.IsDefined(typeof(Attributes.ExtendUserAttribute), false))
                    {
                        var value = property.GetValue(extendedProperties);

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
                throw new OrhAuthException($"UpdateStandardExtendedUser error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves detailed extended user information by user ID.
        /// Internally calls <see cref="GetUserDynamicById(int)"/> to support dynamic user types.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>A dynamic object containing user information, including extended properties if available.</returns>
        public dynamic GetExtendedUserInfo(int userId)
        {
            return GetUserDynamicById(userId);
        }

        #endregion

        #region Dynamic User Get Methods

        /// <summary>
        /// Retrieves a user as a dynamic object based on the specified user ID.
        /// Includes both standard and extended user properties if available.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A dynamic user object or null if not found.</returns>
        public dynamic GetUserDynamicById(int userId)
        {
            return GetUserDynamicByFilter(u => u.Id == userId);
        }

        /// <summary>
        /// Retrieves a user as a dynamic object based on the specified email address.
        /// Includes both standard and extended user properties if available.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A dynamic user object or null if not found.</returns>
        public dynamic GetUserDynamicByEmail(string email)
        {
            return GetUserDynamicByFilter(u => u.Email == email);
        }

        /// <summary>
        /// Retrieves a user as a dynamic object by querying the extended user property "LVUserLogin".
        /// This method is specifically designed for scenarios where users log in using a custom login field.
        /// </summary>
        /// <param name="login">The custom login value (e.g., LVUserLogin).</param>
        /// <returns>A dynamic user object including extended fields, or null if not found.</returns>
        public dynamic GetUserDynamicByValue(string customField, string customValue)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Users WHERE @Field = @Value", connection))
                {
                    command.Parameters.AddWithValue("@Field", customField);
                    command.Parameters.AddWithValue("@Value", customValue);

                    return ReadSingleUserDynamic(command);
                }
            }
        }

        public dynamic GetUserDynamicByValues(string customField1, string customValue1, string customField2, string customValue2)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // 🛠️ Dikkat: Field1 ve Field2 doğrudan SQL'e ekleniyor (parametre yapılamaz!)
                string query = $"SELECT * FROM Users WHERE [{customField1}] = @Value1 AND [{customField2}] = @Value2";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value1", customValue1);
                    command.Parameters.AddWithValue("@Value2", customValue2);

                    return ReadSingleUserDynamic(command);
                }
            }
        }

        /// <summary>
        /// Retrieves a dynamic user object from the database based on the specified filter expression.
        /// The returned object includes both standard User properties and extended fields from the database,
        /// as well as assigned operation claims.
        /// </summary>
        /// <param name="filter">Expression used to filter the user (e.g., by Id, Email, etc.).</param>
        /// <returns>
        /// A dynamic object representing the user, including extended properties and role claims;
        /// returns null if no matching user is found.
        /// </returns>
        public dynamic GetUserDynamicByFilter(Expression<Func<User, bool>> filter)
        {
            using (var context = new AuthDbContext<User>(_connectionString))
            {
                var user = context.Users.AsNoTracking().FirstOrDefault(filter);
                if (user == null)
                    return null;

                var dynamicUser = new ExpandoObject() as IDictionary<string, object>;

                foreach (var property in typeof(User).GetProperties())
                {
                    dynamicUser[property.Name] = property.GetValue(user);
                }

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

                    var operationClaims = new List<dynamic>();

                    string query = @"
                                SELECT uoc.Id, uoc.UserId, uoc.OperationClaimId, oc.[Name] ClaimName 
                                FROM OperationClaims oc
                                INNER JOIN UserOperationClaims uoc ON oc.Id = uoc.OperationClaimId
                                WHERE uoc.IsDeleted = 0 AND uoc.UserId = @UserId";

                    System.Diagnostics.Debug.WriteLine($"Querying permissions for user ID: {user.Id}");

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user.Id);
                        using (var reader = command.ExecuteReader())
                        {
                            int count = 0;
                            while (reader.Read())
                            {
                                count++;
                                var claim = new ExpandoObject() as IDictionary<string, object>;
                                claim["Id"] = reader.GetInt32(reader.GetOrdinal("Id"));
                                claim["UserId"] = reader.GetInt32(reader.GetOrdinal("UserId"));
                                claim["OperationClaimId"] = reader.GetInt32(reader.GetOrdinal("OperationClaimId"));
                                claim["ClaimName"] = reader.GetString(reader.GetOrdinal("ClaimName"));
                                operationClaims.Add(claim);

                            }
                            System.Diagnostics.Debug.WriteLine($"A total of {count} permissions found");
                        }
                    }

                    dynamicUser["UserOperationClaims"] = operationClaims;
                }

                return dynamicUser;
            }
        }

        /// <summary>
        /// Retrieves a list of dynamic user objects that match the specified filter expression.
        /// Each dynamic object includes both standard and extended user fields, as well as assigned role claims.
        /// </summary>
        /// <param name="filter">Expression used to filter users (e.g., by status, email domain, etc.).</param>
        /// <returns>
        /// A list of dynamic user objects enriched with extended properties and associated claims.
        /// Returns an empty list if no users match the filter.
        /// </returns>
        public List<dynamic> GetUsersDynamicByFilter(Expression<Func<User, bool>> filter)
        {
            using (var context = new AuthDbContext<User>(_connectionString))
            {
                var users = context.Users.AsNoTracking().Where(filter).ToList();

                if (users == null || users.Count == 0)
                    return new List<dynamic>();

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

        /// <summary>
        /// Retrieves a dynamic user object with all fields from the database by the specified user ID.
        /// The dynamic object includes both standard user properties and any additional fields available in the database.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve from the database.</param>
        /// <returns>
        /// A dynamic object representing the user with the specified ID, including both standard and extended fields.
        /// Returns null if no user with the given ID is found.
        /// </returns>
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

        /// <summary>
        /// Reads a single user record from the database using the provided SQL command and returns it as a dynamic object.
        /// The dynamic object contains all columns from the database record, with byte arrays converted to Base64 strings.
        /// </summary>
        /// <param name="command">The SQL command to execute, which retrieves the user record from the database.</param>
        /// <returns>
        /// A dynamic object representing the user record with all fields, where byte arrays are converted to Base64 strings.
        /// Returns null if no records are found.
        /// </returns>
        private dynamic ReadSingleUserDynamic(SqlCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    dynamic dynamicUser = new ExpandoObject();
                    var expandoDict = (IDictionary<string, object>)dynamicUser;

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object value = reader.IsDBNull(i) ? null : reader.GetValue(i);

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