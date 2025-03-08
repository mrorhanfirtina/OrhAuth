using OrhAuth.Configurations;
using OrhAuth.Services;

namespace OrhAuth.Extensions
{
    public static class OrhAuthExtensions
    {
        // .NET Framework projeleri için kullanılacak yapılandırma metodu
        public static IAuthService ConfigureOrhAuth(OrhAuthOptions options)
        {
            // Veritabanı başlatma
            AuthFrameworkInitializer.Initialize(
                options.ConnectionString,
                options.CreateDatabaseIfNotExists,
                options.ExtendedUserType  // ExtendedUserType'ı da iletiyoruz
            );

            // Auth servisi oluştur ve döndür
            return AuthFrameworkInitializer.CreateAuthService(options.ConnectionString, options);
        }
    }
}
