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
    public class AuthDbContext : DbContext
    {
        /// <summary>
        /// Lock object used to ensure thread-safe access to shared resources during model configuration and metadata caching.
        /// </summary>
        private static readonly object _lockObject = new object();

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

        /// <summary>
        /// Static constructor to configure the database initializer. 
        /// Ensures that the database is automatically created if it does not already exist.
        /// </summary>
        static AuthDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AuthDbContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Configures the model by registering all EntityTypeConfiguration classes in the assembly
        /// and applying dynamic property configurations for extended user models.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance used for configuring the model.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            try
            {
                RegisterExtendedProperties(modelBuilder);
            }
            catch (Exception ex)
            {
                throw new OrhAuthException("An error occurred while registering extended properties in the model builder.", ex);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Registers and configures extended user properties dynamically using reflection and expression trees.
        /// Properties must be decorated with the ExtendUserAttribute.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance used for dynamic configuration.</param>
        /// <exception cref="InvalidOperationException">Thrown if a property cannot be configured dynamically.</exception>
        private void RegisterExtendedProperties(DbModelBuilder modelBuilder)
        {
            try
            {
                var userType = typeof(User);
                var extendedProperties = SchemaMetadataCache.GetExtendedProperties(userType);

                if (extendedProperties.Count == 0)
                {
                    throw new OrhAuthException("No extended properties found on the extended User type. Please ensure that properties are decorated with [ExtendUser].");
                }

                System.Diagnostics.Debug.WriteLine($"Bulunan genişletilmiş özellik sayısı: {extendedProperties.Count}");

                var entityTypeConfiguration = modelBuilder.Entity<User>();

                foreach (var metadata in extendedProperties)
                {
                    var property = metadata.Property;
                    var attribute = metadata.Attribute;

                    try
                    {
                        string propertyName = property.Name;
                        System.Diagnostics.Debug.WriteLine($"İşleniyor: {propertyName}, Tip: {property.PropertyType.Name}");

                        var parameterExp = System.Linq.Expressions.Expression.Parameter(typeof(User), "e");
                        var propertyExp = System.Linq.Expressions.Expression.Property(parameterExp, propertyName);
                        var lambdaExp = System.Linq.Expressions.Expression.Lambda(propertyExp, parameterExp);

                        var propertyMethod = typeof(EntityTypeConfiguration<User>)
                            .GetMethods()
                            .Where(m => m.Name == "Property" && m.IsGenericMethod)
                            .FirstOrDefault();

                        if (propertyMethod == null)
                        {
                            throw new InvalidOperationException("Property metodu bulunamadı!");
                        }

                        var genericMethod = propertyMethod.MakeGenericMethod(property.PropertyType);
                        var propertyConfiguration = genericMethod.Invoke(entityTypeConfiguration, new object[] { lambdaExp });

                        var configType = propertyConfiguration.GetType();

                        if (attribute.MaxLength > 0 && property.PropertyType == typeof(string))
                        {
                            var hasMaxLengthMethod = configType.GetMethod("HasMaxLength", new[] { typeof(int) });
                            if (hasMaxLengthMethod != null)
                            {
                                hasMaxLengthMethod.Invoke(propertyConfiguration, new object[] { attribute.MaxLength });
                                System.Diagnostics.Debug.WriteLine($"  HasMaxLength({attribute.MaxLength}) uygulandı");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("  HATA: HasMaxLength metodu bulunamadı!");
                            }
                        }

                        if (attribute.IsRequired)
                        {
                            var isRequiredMethod = configType.GetMethod("IsRequired");
                            if (isRequiredMethod != null)
                            {
                                isRequiredMethod.Invoke(propertyConfiguration, null);
                                System.Diagnostics.Debug.WriteLine($"  IsRequired() uygulandı");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("  HATA: IsRequired metodu bulunamadı!");
                            }
                        }
                        else
                        {
                            var isOptionalMethod = configType.GetMethod("IsOptional");
                            if (isOptionalMethod != null)
                            {
                                isOptionalMethod.Invoke(propertyConfiguration, null);
                                System.Diagnostics.Debug.WriteLine($"  IsOptional() uygulandı");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("  HATA: IsOptional metodu bulunamadı!");
                            }
                        }

                        if (!string.IsNullOrEmpty(attribute.DbType))
                        {
                            var columnTypeMethod = configType.GetMethod("HasColumnType", new[] { typeof(string) });
                            if (columnTypeMethod != null)
                            {
                                columnTypeMethod.Invoke(propertyConfiguration, new[] { attribute.DbType });
                                System.Diagnostics.Debug.WriteLine($"  HasColumnType({attribute.DbType}) uygulandı");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("  HATA: HasColumnType metodu bulunamadı!");
                            }
                        }

                        if (attribute.IsUnique)
                        {
                            System.Diagnostics.Debug.WriteLine($"  Unique property: {propertyName} - Migration veya SQL ile unique index oluşturulmalıdır.");
                        }

                        // Log
                        System.Diagnostics.Debug.WriteLine($"Eklendi: {property.Name} (MaxLength: {attribute.MaxLength}, Required: {attribute.IsRequired}, Unique: {attribute.IsUnique})");
                    }
                    catch (Exception ex)
                    {
                        throw new OrhAuthException($"An error occurred while adding extended property '{property.Name}': {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new OrhAuthException($"An error occurred while registering extended user properties: {ex.Message}", ex);
            }
        }
    }
}