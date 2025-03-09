using OrhAuth.Attributes;
using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Concrete;
using OrhAuth.Models.Entities;
using OrhAuth.Security.JWT;
using OrhAuth.Services;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace OrhAuth.Configurations
{
    public static class AuthFrameworkInitializer
    {
        public static void Initialize(string connectionString, bool createDatabaseIfNotExists = true, System.Type extendedUserType = null)
        {
            // Önce tip kaydını yap
            if (extendedUserType != null)
            {
                SchemaMetadataCache.RegisterExtendedType(extendedUserType);
            }

            using (var context = new AuthDbContext(connectionString))
            {
                // Veritabanı yoksa ve oluşturma flag'i true ise oluştur
                if (!context.Database.Exists() && createDatabaseIfNotExists)
                {
                    context.Database.Create();
                    SeedInitialData(context);

                    // Genişletilmiş alanları ekle
                    if (extendedUserType != null)
                    {
                        AddExtendedColumnsWithSQL(context, extendedUserType);
                    }
                }
                // Veritabanı varsa ve genişletilmiş tip tanımlanmışsa, sütunları ekle
                else if (context.Database.Exists() && extendedUserType != null)
                {
                    AddExtendedColumnsWithSQL(context, extendedUserType);
                }
            }
        }

        private static void AddExtendedColumnsWithSQL(AuthDbContext context, Type extendedUserType)
        {
            try
            {
                var properties = extendedUserType.GetProperties()
                    .Where(p => p.IsDefined(typeof(ExtendUserAttribute), false))
                    .ToList();

                if (properties.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Genişletilmiş özellik bulunamadı, SQL ile kolon eklenmiyor.");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"SQL ile eklenecek kolon sayısı: {properties.Count}");

                using (var connection = context.Database.Connection)
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        foreach (var prop in properties)
                        {
                            var attribute = (ExtendUserAttribute)prop.GetCustomAttribute(typeof(ExtendUserAttribute));
                            string sqlType = GetSqlDataType(prop.PropertyType, attribute);

                            // DEFAULT değer kısmını oluştur
                            string defaultValueClause = "";
                            if (!string.IsNullOrEmpty(attribute.DefaultValue))
                            {
                                if (prop.PropertyType == typeof(string))
                                    defaultValueClause = $" DEFAULT '{attribute.DefaultValue}'";
                                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(decimal) ||
                                        prop.PropertyType == typeof(float) || prop.PropertyType == typeof(double))
                                    defaultValueClause = $" DEFAULT {attribute.DefaultValue}";
                                else if (prop.PropertyType == typeof(bool))
                                    defaultValueClause = $" DEFAULT {(attribute.DefaultValue.ToLower() == "true" ? "1" : "0")}";
                                else if (prop.PropertyType == typeof(DateTime))
                                    defaultValueClause = $" DEFAULT '{attribute.DefaultValue}'";
                                else if (prop.PropertyType == typeof(Guid))
                                    defaultValueClause = $" DEFAULT '{attribute.DefaultValue}'";
                            }

                            // NOT NULL kısıtı sadece DEFAULT değer varsa veya NULL izin veriliyorsa eklenebilir
                            string nullableSuffix = attribute.IsRequired && !string.IsNullOrEmpty(attribute.DefaultValue) ?
                                                  " NOT NULL" : " NULL";

                            // Kolon veritabanında var mı diye kontrol ederek ekle
                            string alterSql = $"IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = '{prop.Name}') " +
                                             $"ALTER TABLE [dbo].[Users] ADD [{prop.Name}] {sqlType}{defaultValueClause}{nullableSuffix}";

                            command.CommandText = alterSql;
                            command.ExecuteNonQuery();

                            System.Diagnostics.Debug.WriteLine($"SQL ile eklendi: {prop.Name} (Type: {sqlType}{defaultValueClause}{nullableSuffix})");

                            // Eğer required alan eklenmiş ve default değeri yoksa, mevcut kayıtları güncelle
                            if (attribute.IsRequired && string.IsNullOrEmpty(attribute.DefaultValue) &&
                                (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(int)))
                            {
                                string updateValue = prop.PropertyType == typeof(string) ? "''" : "0";
                                string updateSql = $"UPDATE [dbo].[Users] SET [{prop.Name}] = {updateValue} WHERE [{prop.Name}] IS NULL";
                                command.CommandText = updateSql;
                                command.ExecuteNonQuery();

                                // Şimdi NOT NULL olarak değiştir
                                string alterColumnSql = $"ALTER TABLE [dbo].[Users] ALTER COLUMN [{prop.Name}] {sqlType} NOT NULL";
                                command.CommandText = alterColumnSql;
                                command.ExecuteNonQuery();

                                System.Diagnostics.Debug.WriteLine($"SQL ile NOT NULL yapıldı: {prop.Name}");
                            }

                            // Eğer Unique kısıtı varsa index ekle
                            if (attribute.IsUnique)
                            {
                                string indexName = $"IX_Users_{prop.Name}_Unique";
                                string indexSql = $"IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = '{indexName}' AND object_id = OBJECT_ID(N'[dbo].[Users]')) " +
                                                 $"CREATE UNIQUE NONCLUSTERED INDEX [{indexName}] ON [dbo].[Users] ([{prop.Name}]) WHERE [{prop.Name}] IS NOT NULL";

                                command.CommandText = indexSql;
                                command.ExecuteNonQuery();

                                System.Diagnostics.Debug.WriteLine($"SQL ile unique index eklendi: {indexName}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL ile kolon eklenirken hata: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Hata detayı: {ex.StackTrace}");
                throw; // Hatayı yukarı fırlat ki kullanıcı görebilsin
            }
        }

        private static string GetSqlDataType(Type propType, ExtendUserAttribute attribute)
        {
            if (!string.IsNullOrEmpty(attribute.DbType))
                return attribute.DbType;

            if (propType == typeof(string))
                return attribute.MaxLength > 0
                            ? $"nvarchar({attribute.MaxLength})"
                            : "nvarchar(max)";
            else if (propType == typeof(int) || propType == typeof(int?))
                return "int";
            else if (propType == typeof(decimal) || propType == typeof(decimal?))
                return "decimal(18,2)";
            else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                return "datetime2";
            else if (propType == typeof(bool) || propType == typeof(bool?))
                return "bit";
            else if (propType == typeof(Guid) || propType == typeof(Guid?))
                return "uniqueidentifier";
            else if (propType == typeof(byte[]))
                return "varbinary(max)";
            else if (propType == typeof(float) || propType == typeof(float?))
                return "float";
            else if (propType == typeof(double) || propType == typeof(double?))
                return "float";
            else
                return "nvarchar(max)";
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
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true
            };

            context.Users.Add(adminUser);
            context.SaveChanges();

            // Admin rolü bağlantısını ekle
            context.UserOperationClaims.Add(new UserOperationClaim
            {
                UserId = adminUser.Id,
                OperationClaimId = adminRole.Id
            });

            context.SaveChanges();
        }

        public static IAuthService CreateAuthService(string connectionString, OrhAuthOptions options)
        {
            var tokenOptions = new TokenOptions
            {
                Audience = options.TokenAudience,
                Issuer = options.TokenIssuer,
                AccessTokenExpiration = options.TokenExpirationMinutes,
                SecurityKey = options.TokenSecurityKey,
                RefreshTokenTTL = options.RefreshTokenTTLDays
            };

            // Repository ve service oluşturma
            var context = new AuthDbContext(connectionString);

            // Generic repository base kullanarak repository'leri oluştur
            var userRepository = new EfEntityRepositoryBase<User>(context);
            var operationClaimRepository = new EfEntityRepositoryBase<OperationClaim>(context);
            var userOperationClaimRepository = new EfEntityRepositoryBase<UserOperationClaim>(context);
            var refreshTokenRepository = new EfEntityRepositoryBase<RefreshToken>(context);

            // TokenHelper'ı oluştur
            var tokenHelper = new JwtHelper(
                tokenOptions.SecurityKey,
                tokenOptions.Issuer,
                tokenOptions.Audience,
                tokenOptions.AccessTokenExpiration);

            // AuthService'i oluştur
            var authService = new AuthManager(
                userRepository,
                operationClaimRepository,
                userOperationClaimRepository,
                tokenHelper);

            return authService;
        }
    }
}