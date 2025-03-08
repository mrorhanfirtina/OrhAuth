using OrhAuth.Models.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace OrhAuth.Data.Context
{
    public class AuthDbContext : DbContext
    {
        // Statik değişken kaldırıldı, her OnModelCreating çağrısında genişletilmiş özellikleri ekleyeceğiz
        private static readonly object _lockObject = new object();

        public AuthDbContext() : base("name=AuthDbConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public AuthDbContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        // DbContext'in statik yapıcısında bu işlemi yap
        static AuthDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AuthDbContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Assembly içindeki tüm EntityTypeConfiguration sınıflarını otomatik olarak kaydet
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

            // Dinamik alanları eklemek için mevcut mekanizmayı kullan
            try
            {
                RegisterExtendedProperties(modelBuilder);
            }
            catch (Exception ex)
            {
                // Hata durumunu düzgün şekilde logla
            }

            base.OnModelCreating(modelBuilder);
        }

        private void RegisterExtendedProperties(DbModelBuilder modelBuilder)
        {
            try
            {
                var userType = typeof(User);
                var extendedProperties = SchemaMetadataCache.GetExtendedProperties(userType);

                if (extendedProperties.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Genişletilmiş özellik bulunamadı!");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Bulunan genişletilmiş özellik sayısı: {extendedProperties.Count}");

                // Entity type builder'ı al
                var entityTypeConfiguration = modelBuilder.Entity<User>();

                foreach (var metadata in extendedProperties)
                {
                    var property = metadata.Property;
                    var attribute = metadata.Attribute;

                    try
                    {
                        // Bu yöntemle dinamik property'leri konfigüre edebiliriz
                        string propertyName = property.Name;
                        System.Diagnostics.Debug.WriteLine($"İşleniyor: {propertyName}, Tip: {property.PropertyType.Name}");

                        // EF6'da dinamik property yapılandırması için expression kullanılmalı
                        var parameterExp = System.Linq.Expressions.Expression.Parameter(typeof(User), "e");
                        var propertyExp = System.Linq.Expressions.Expression.Property(parameterExp, propertyName);
                        var lambdaExp = System.Linq.Expressions.Expression.Lambda(propertyExp, parameterExp);

                        // Property metodunu bul
                        var propertyMethod = typeof(EntityTypeConfiguration<User>)
                            .GetMethods()
                            .Where(m => m.Name == "Property" && m.IsGenericMethod)
                            .FirstOrDefault();

                        if (propertyMethod == null)
                        {
                            throw new InvalidOperationException("Property metodu bulunamadı!");
                        }

                        // Generic method'u özellik tipi ile çağır
                        var genericMethod = propertyMethod.MakeGenericMethod(property.PropertyType);
                        var propertyConfiguration = genericMethod.Invoke(entityTypeConfiguration, new object[] { lambdaExp });

                        // Dönüş tipi StringPropertyConfiguration, DecimalPropertyConfiguration, vs. olabilir
                        var configType = propertyConfiguration.GetType();

                        // MaxLength yapılandırması
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

                        // IsRequired yapılandırması
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

                        // DbType yapılandırması
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

                        // Index oluşturma - ayrı bir işlem olarak yapılır
                        if (attribute.IsUnique)
                        {
                            // EF6'da index oluşturma için fluent API kullanmak daha karmaşık, 
                            // bunun yerine basitleştirilmiş bir yaklaşım kullanalım:
                            System.Diagnostics.Debug.WriteLine($"  Unique property: {propertyName} - Migration veya SQL ile unique index oluşturulmalıdır.");
                        }

                        // Loglama
                        System.Diagnostics.Debug.WriteLine($"Eklendi: {property.Name} (MaxLength: {attribute.MaxLength}, Required: {attribute.IsRequired}, Unique: {attribute.IsUnique})");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Özellik eklenirken hata: {property.Name} - {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RegisterExtendedProperties genel hatası: {ex.Message}");
                throw;
            }
        }
    }
}