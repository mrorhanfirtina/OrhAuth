using OrhAuth.Data.Repositories.Abstract;
using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using OrhAuth.Security.Hashing;
using OrhAuth.Security.JWT;
using System.Collections.Generic;
using System;

namespace OrhAuth.Services
{
    public class AuthManager : IAuthService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<OperationClaim> _operationClaimRepository;
        private readonly IEntityRepository<UserOperationClaim> _userOperationClaimRepository;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(
            IEntityRepository<User> userRepository,
            IEntityRepository<OperationClaim> operationClaimRepository,
            IEntityRepository<UserOperationClaim> userOperationClaimRepository,
            ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
        }

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
    }
}
