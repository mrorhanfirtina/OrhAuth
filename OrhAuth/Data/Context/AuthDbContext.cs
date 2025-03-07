using OrhAuth.Models.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace OrhAuth.Data.Context
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext() : base("name=AuthDbConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public AuthDbContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id)
                .Property(u => u.Email).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<OperationClaim>()
                .HasKey(o => o.Id)
                .Property(o => o.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<UserOperationClaim>()
                .HasKey(uo => uo.Id);

            modelBuilder.Entity<UserOperationClaim>()
                .HasRequired(uo => uo.User)
                .WithMany(u => u.UserOperationClaims)
                .HasForeignKey(uo => uo.UserId);

            modelBuilder.Entity<UserOperationClaim>()
                .HasRequired(uo => uo.OperationClaim)
                .WithMany(o => o.UserOperationClaims)
                .HasForeignKey(uo => uo.OperationClaimId);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(rt => rt.Id);

            modelBuilder.Entity<RefreshToken>()
                .HasRequired(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);

            base.OnModelCreating(modelBuilder);
            // Dinamik alanları tarar ve ekler
            RegisterExtendedProperties(modelBuilder);
        }

        private void RegisterExtendedProperties(DbModelBuilder modelBuilder)
        {
            var userType = typeof(User);
            var extendedProperties = SchemaMetadataCache.GetExtendedProperties(userType);

            // Entity type builder'ı al
            var entityTypeConfiguration = modelBuilder.Entity<User>();

            foreach (var metadata in extendedProperties)
            {
                var property = metadata.Property;
                var attribute = metadata.Attribute;

                try
                {
                    // Bu yöntemle dinamik property'leri konfigüre edebiliriz
                    // Önce mevcut özellikleri almamız gerekiyor
                    string propertyName = property.Name;

                    // EF6'da dinamik property yapılandırması için string değil expression kullanılmalı
                    // bunun için string property adını kullanarak reflection ile expression oluşturuyoruz
                    var parameterExp = System.Linq.Expressions.Expression.Parameter(typeof(User), "e");
                    var propertyExp = System.Linq.Expressions.Expression.Property(parameterExp, propertyName);
                    var lambdaExp = System.Linq.Expressions.Expression.Lambda(propertyExp, parameterExp);

                    // Dynamic Invoke ile property yapılandırması için gerekli lambda ifadesini oluşturuyoruz
                    var propertyMethod = typeof(EntityTypeConfiguration<User>)
                        .GetMethods()
                        .Where(m => m.Name == "Property" && m.IsGenericMethod)
                        .FirstOrDefault();

                    if (propertyMethod != null)
                    {
                        // Generic method'u özellik tipi ile çağırıyoruz
                        var genericMethod = propertyMethod.MakeGenericMethod(property.PropertyType);
                        var propertyConfiguration = genericMethod.Invoke(entityTypeConfiguration, new[] { lambdaExp });

                        // PropertyConfiguration üzerindeki metodları çağırmak için reflection kullanıyoruz
                        var configType = propertyConfiguration.GetType();

                        // MaxLength yapılandırması
                        if ((property.PropertyType == typeof(string) || property.PropertyType == typeof(byte[])) && attribute.MaxLength > 0)
                        {
                            var maxLengthMethod = configType.GetMethod("HasMaxLength", new[] { typeof(int) });
                            if (maxLengthMethod != null)
                            {
                                maxLengthMethod.Invoke(propertyConfiguration, new object[] { attribute.MaxLength });
                            }
                        }

                        // IsRequired yapılandırması
                        if (attribute.IsRequired)
                        {
                            var isRequiredMethod = configType.GetMethod("IsRequired");
                            if (isRequiredMethod != null)
                            {
                                isRequiredMethod.Invoke(propertyConfiguration, null);
                            }
                        }
                        else
                        {
                            var isOptionalMethod = configType.GetMethod("IsOptional");
                            if (isOptionalMethod != null)
                            {
                                isOptionalMethod.Invoke(propertyConfiguration, null);
                            }
                        }

                        // DbType yapılandırması
                        if (!string.IsNullOrEmpty(attribute.DbType))
                        {
                            var columnTypeMethod = configType.GetMethod("HasColumnType", new[] { typeof(string) });
                            if (columnTypeMethod != null)
                            {
                                columnTypeMethod.Invoke(propertyConfiguration, new[] { attribute.DbType });
                            }
                        }
                    }

                    // Index oluşturma - ayrı bir işlem olarak yapılır
                    if (attribute.IsUnique)
                    {
                        // EF6'da index oluşturma için fluent API kullanmak daha karmaşık, 
                        // bunun yerine basitleştirilmiş bir yaklaşım kullanalım:
                        // SQL migration'ı sırasında veya custom SQL ile yapılabilir
                        System.Diagnostics.Debug.WriteLine($"Unique property: {propertyName} - Migration veya SQL ile unique index oluşturulmalıdır.");
                    }

                    // Loglama
                    System.Diagnostics.Debug.WriteLine($"Eklendi: {property.Name} (MaxLength: {attribute.MaxLength}, Required: {attribute.IsRequired}, Unique: {attribute.IsUnique})");
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama 
                    System.Diagnostics.Debug.WriteLine($"Hata: {property.Name} özelliği eklenirken hata oluştu: {ex.Message}");
                }
            }
        }
    }
}