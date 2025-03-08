using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Concrete;
using OrhAuth.Models.Entities;
using OrhAuth.Security.JWT;
using OrhAuth.Services;

namespace OrhAuth.Configurations
{
    public static class AuthFrameworkInitializer
    {
        public static void Initialize(string connectionString, bool createDatabaseIfNotExists = true, System.Type extendedUserType = null)
        {

            // Önce genişletilmiş tip kaydedilmeli
            if (extendedUserType != null)
            {
                SchemaMetadataCache.RegisterExtendedType(extendedUserType);
            }

            if (createDatabaseIfNotExists)
            {
                using (var context = new AuthDbContext(connectionString))
                {
                    if (context.Database.Exists())
                    {
                        // Database değişikliklerini uygula
                        context.Database.Initialize(force: false);
                    }
                    else
                    {
                        // Veritabanını oluştur
                        context.Database.Create();
                        SeedInitialData(context);
                    }
                }
            }
        }

        private static void SeedInitialData(AuthDbContext context)
        {
            // Admin rolünü ekle
            var adminRole = new OperationClaim { Name = "Admin" };
            context.OperationClaims.Add(adminRole);

            // Kullanıcı rolünü ekle
            var userRole = new OperationClaim { Name = "User" };
            context.OperationClaims.Add(userRole);

            context.SaveChanges();

            // Admin kullanıcısını oluştur
            byte[] passwordHash, passwordSalt;
            Security.Hashing.HashingHelper.CreatePasswordHash("Admin123!", out passwordHash, out passwordSalt);

            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
                LocalityId = "1" // Varsayılan locality
            };

            context.Users.Add(adminUser);
            context.SaveChanges();

            // Admin kullanıcısına Admin rolünü ver
            var userOperationClaim = new UserOperationClaim
            {
                UserId = adminUser.Id,
                OperationClaimId = adminRole.Id
            };

            context.UserOperationClaims.Add(userOperationClaim);
            context.SaveChanges();
        }

        public static IAuthService CreateAuthService(string connectionString, OrhAuthOptions options = null)
        {
            var context = new AuthDbContext(connectionString);
            var userRepository = new EfEntityRepositoryBase<User>(context);
            var operationClaimRepository = new EfEntityRepositoryBase<OperationClaim>(context);
            var userOperationClaimRepository = new EfEntityRepositoryBase<UserOperationClaim>(context);

            var tokenHelper = options == null ?
                new JwtHelper() :
                new JwtHelper(options.TokenSecurityKey,
                              options.TokenIssuer,
                              options.TokenAudience,
                              options.TokenExpirationMinutes);

            return new AuthManager(userRepository, operationClaimRepository, userOperationClaimRepository, tokenHelper);
        }
    }
}
