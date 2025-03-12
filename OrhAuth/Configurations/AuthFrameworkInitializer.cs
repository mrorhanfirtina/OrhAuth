
using OrhAuth.Attributes;
using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Concrete;
using OrhAuth.Models.Entities;
using OrhAuth.Security.Hashing;
using OrhAuth.Security.JWT;
using OrhAuth.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OrhAuth.Configurations
{
    public static class AuthFrameworkInitializer
    {
        /// <summary>
        /// OrhAuth framework'ünü başlatır, veritabanını oluşturur ve genişletilmiş alanları ekler
        /// </summary>
        /// <param name="connectionString">Veritabanı bağlantı dizesi</param>
        /// <param name="createDatabaseIfNotExists">Veritabanı yoksa oluşturulsun mu?</param>
        /// <param name="extendedUserType">Genişletilmiş User tipi (varsa)</param>
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

        /// <summary>
        /// Genişletilmiş User tipindeki özellikleri SQL ile Users tablosuna ekler
        /// </summary>
        private static void AddExtendedColumnsWithSQL(AuthDbContext context, Type extendedUserType)
        {
            try
            {
                var properties = extendedUserType.GetProperties()
                    .Where(p => p.IsDefined(typeof(ExtendUserAttribute), false))
                    .ToList();

                if (properties.Count == 0)
                    return;

                // Mevcut sütunları al
                var existingColumns = new List<string>();
                using (var command = context.Database.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users'";
                    context.Database.Connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            existingColumns.Add(reader.GetString(0));
                        }
                    }
                    context.Database.Connection.Close();
                }

                // Her bir özellik için sütun ekle
                foreach (var property in properties)
                {
                    // Sütun adı
                    string columnName = property.Name;

                    // Sütun zaten varsa atla
                    if (existingColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase))
                        continue;

                    // SQL veri tipini belirle
                    string sqlType = GetSqlType(property.PropertyType);

                    // ALTER TABLE komutu oluştur
                    string alterTableSql = $"ALTER TABLE Users ADD {columnName} {sqlType} NULL";

                    // Komutu çalıştır
                    context.Database.ExecuteSqlCommand(alterTableSql);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapılabilir
                System.Diagnostics.Debug.WriteLine($"Extended columns could not be added: {ex.Message}");
            }
        }

        /// <summary>
        /// C# tipini SQL tipine dönüştürür
        /// </summary>
        private static string GetSqlType(Type type)
        {
            if (type == typeof(string))
                return "NVARCHAR(MAX)";
            else if (type == typeof(int) || type == typeof(int?))
                return "INT";
            else if (type == typeof(long) || type == typeof(long?))
                return "BIGINT";
            else if (type == typeof(decimal) || type == typeof(decimal?))
                return "DECIMAL(18, 2)";
            else if (type == typeof(double) || type == typeof(double?))
                return "FLOAT";
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
                return "DATETIME";
            else if (type == typeof(bool) || type == typeof(bool?))
                return "BIT";
            else if (type == typeof(Guid) || type == typeof(Guid?))
                return "UNIQUEIDENTIFIER";
            else if (type == typeof(byte[]))
                return "VARBINARY(MAX)";
            else
                return "NVARCHAR(MAX)";
        }

        /// <summary>
        /// Veritabanına ilk verileri ekler
        /// </summary>
        private static void SeedInitialData(AuthDbContext context)
        {
            try
            {
                // Admin rolü ekle
                var adminRole = new OperationClaim { Name = "admin" };
                context.OperationClaims.Add(adminRole);
                context.SaveChanges();

                // Varsayılan admin kullanıcısı ekle
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("Admin123!", out passwordHash, out passwordSalt);

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

                // Admin kullanıcısına admin rolü ata
                var userRole = new UserOperationClaim
                {
                    UserId = adminUser.Id,
                    OperationClaimId = adminRole.Id
                };
                context.UserOperationClaims.Add(userRole);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapılabilir
                System.Diagnostics.Debug.WriteLine($"Initial data could not be seeded: {ex.Message}");
            }
        }

        /// <summary>
        /// AuthService oluşturur ve döndürür
        /// </summary>
        public static IAuthService CreateAuthService(string connectionString, OrhAuthOptions options)
        {
            // TokenOptions oluştur
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

            // TokenHelper oluştur
            var tokenHelper = new JwtHelper(
                options.TokenSecurityKey,
                options.TokenIssuer,
                options.TokenAudience,
                options.TokenExpirationMinutes);

            // AuthManager oluştur ve connection string'i ilet
            return new AuthManager(
                userRepository,
                operationClaimRepository,
                userOperationClaimRepository,
                refreshTokenRepository,
                tokenHelper,
                connectionString); // Connection string'i AuthManager'a ilet
        }

        /// <summary>
        /// Veritabanı bağlantısını test eder
        /// </summary>
        public static bool TestDatabaseConnection(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Veritabanı şemasını günceller (yeni sütunlar ekler)
        /// </summary>
        public static void UpdateDatabaseSchema(string connectionString, Type extendedUserType)
        {
            if (extendedUserType == null)
                return;

            using (var context = new AuthDbContext(connectionString))
            {
                if (context.Database.Exists())
                {
                    AddExtendedColumnsWithSQL(context, extendedUserType);
                }
            }
        }

        /// <summary>
        /// Veritabanını sıfırlar (siler ve yeniden oluşturur)
        /// </summary>
        public static void ResetDatabase(string connectionString, Type extendedUserType = null)
        {
            using (var context = new AuthDbContext(connectionString))
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }

                context.Database.Create();
                SeedInitialData(context);

                if (extendedUserType != null)
                {
                    AddExtendedColumnsWithSQL(context, extendedUserType);
                }
            }
        }
    }
}
