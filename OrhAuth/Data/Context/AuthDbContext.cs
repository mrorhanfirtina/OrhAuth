using OrhAuth.Exceptions;
using OrhAuth.Models.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace OrhAuth.Data.Context
{
    /// <summary>
    /// Represents the Entity Framework DbContext for OrhAuth library. 
    /// Manages database access, model configurations, and dynamic property registration for extended user entities.
    /// </summary>
    public class AuthDbContext<TUser> : DbContext where TUser : User
    {
        /// <summary>
        /// Initializes a new instance of the AuthDbContext using the default connection string defined in the config file.
        /// </summary>
        public AuthDbContext() : base("name=AuthDbConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the AuthDbContext using the provided connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the target database.</param>
        public AuthDbContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<TUser> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Configures the model by registering all EntityTypeConfiguration classes in the assembly.
        /// Extended properties are handled via SQL column addition in AuthFrameworkInitializer.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance used for configuring the model.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Register standard entity configurations
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
                       type.BaseType != null &&
                       type.BaseType.IsGenericType &&
                       type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            // Note: Extended properties are handled via SQL ALTER TABLE commands in AuthFrameworkInitializer
            // This approach is more reliable than dynamic Entity Framework configuration

            base.OnModelCreating(modelBuilder);
        }
    }
}