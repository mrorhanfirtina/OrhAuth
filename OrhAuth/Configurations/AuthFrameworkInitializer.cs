
using OrhAuth.Attributes;
using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Concrete;
using OrhAuth.Exceptions;
using OrhAuth.Models.Entities;
using OrhAuth.Security.Hashing;
using OrhAuth.Security.JWT;
using OrhAuth.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OrhAuth.Configurations
{
    /// <summary>
    /// Provides startup utilities for initializing the OrhAuth framework.
    /// This includes database creation, extended user column registration, schema updates, and service instantiation.
    /// </summary>
    public static class AuthFrameworkInitializer
    {
        /// <summary>
        /// Initializes the OrhAuth framework by creating the database (if necessary),
        /// registering the extended user model, and applying schema modifications.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="createDatabaseIfNotExists">Specifies whether to create the database if it does not exist.</param>
        /// <param name="extendedUserType">The extended user type to apply custom fields to the schema.</param>
        public static void Initialize(string connectionString, bool createDatabaseIfNotExists = true, System.Type extendedUserType = null)
        {
            if (extendedUserType != null)
            {
                SchemaMetadataCache.RegisterExtendedType(extendedUserType);
            }

            using (var context = new AuthDbContext(connectionString))
            {
                if (!context.Database.Exists() && createDatabaseIfNotExists)
                {
                    context.Database.Create();
                    SeedInitialData(context);

                    if (extendedUserType != null)
                    {
                        AddExtendedColumnsWithSQL(context, extendedUserType);
                    }
                }
                else if (context.Database.Exists() && extendedUserType != null)
                {
                    AddExtendedColumnsWithSQL(context, extendedUserType);
                }
            }
        }

        /// <summary>
        /// Adds extended user properties as SQL columns in the Users table based on the provided user type.
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

                foreach (var property in properties)
                {
                    string columnName = property.Name;

                    if (existingColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase))
                        continue;

                    string sqlType = GetSqlType(property.PropertyType);

                    string alterTableSql = $"ALTER TABLE Users ADD {columnName} {sqlType} NULL";

                    context.Database.ExecuteSqlCommand(alterTableSql);
                }
            }
            catch (Exception ex)
            {
                throw new OrhAuthException($"Extended column creation failed for type {extendedUserType?.Name}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Maps a C# property type to a corresponding SQL Server column type.
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
        /// Seeds initial data such as the default admin role and admin user into the database.
        /// </summary>
        private static void SeedInitialData(AuthDbContext context)
        {
            try
            {
                var adminRole = new OperationClaim { Name = "admin" };
                context.OperationClaims.Add(adminRole);
                context.SaveChanges();

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
                throw new OrhAuthException("Failed to seed initial data to the database.", ex);
            }
        }



        /// <summary>
        /// Creates and returns an instance of <see cref="IAuthService"/> with the provided configuration options.
        /// Initializes all necessary repositories, the JWT token helper, and the AuthManager.
        /// </summary>
        /// <param name="connectionString">The database connection string to be used by the repositories.</param>
        /// <param name="options">An <see cref="OrhAuthOptions"/> object that contains token and user settings.</param>
        /// <returns>An initialized implementation of <see cref="IAuthService"/> ready to be used in authentication flows.</returns>

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

            var context = new AuthDbContext(connectionString);

            var userRepository = new EfEntityRepositoryBase<User>(context);
            var operationClaimRepository = new EfEntityRepositoryBase<OperationClaim>(context);
            var userOperationClaimRepository = new EfEntityRepositoryBase<UserOperationClaim>(context);
            var refreshTokenRepository = new EfEntityRepositoryBase<RefreshToken>(context);

            var tokenHelper = new JwtHelper(
                options.TokenSecurityKey,
                options.TokenIssuer,
                options.TokenAudience,
                options.TokenExpirationMinutes);

            return new AuthManager(
                userRepository,
                operationClaimRepository,
                userOperationClaimRepository,
                refreshTokenRepository,
                tokenHelper,
                connectionString);
        }


        /// <summary>
        /// Tests whether a connection to the database can be successfully established using the given connection string.
        /// </summary>
        /// <param name="connectionString">The connection string used to attempt the database connection.</param>
        /// <returns>
        /// <c>true</c> if the connection to the database is successful; otherwise, <c>false</c>.
        /// </returns>
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
        /// Updates the database schema by adding new columns to the <c>Users</c> table
        /// based on the provided extended user type.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="extendedUserType">
        /// The extended user class type that contains additional properties decorated with <see cref="ExtendUserAttribute"/>.
        /// </param>
        /// <remarks>
        /// This method checks whether the database exists and, if so, adds the extended user fields as new columns.
        /// If <paramref name="extendedUserType"/> is <c>null</c>, the method exits without performing any operation.
        /// </remarks>
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
        /// Completely resets the database by deleting and recreating it,
        /// including initial data and any extended user schema if specified.
        /// </summary>
        /// <param name="connectionString">The connection string used to connect to the database.</param>
        /// <param name="extendedUserType">
        /// (Optional) A type that extends the <c>User</c> entity with additional properties.
        /// If provided, new columns defined by <see cref="ExtendUserAttribute"/> will be added to the <c>Users</c> table.
        /// </param>
        /// <remarks>
        /// This method should only be used in development or testing environments.
        /// It deletes all existing data in the database and resets it to the initial OrhAuth schema.
        /// </remarks>
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
