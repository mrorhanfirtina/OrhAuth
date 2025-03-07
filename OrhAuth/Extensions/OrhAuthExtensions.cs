using OrhAuth.Configurations;
using OrhAuth.Data.Context;
using OrhAuth.Data.Repositories.Abstract;
using OrhAuth.Data.Repositories.Concrete;
using OrhAuth.Models.Entities;
using OrhAuth.Security.JWT;
using OrhAuth.Services;
using System;

namespace OrhAuth.Extensions
{
    public static class OrhAuthExtensions
    {
        // .NET Framework projeleri için kullanılacak yapılandırma metodu
        public static IAuthService ConfigureOrhAuth(OrhAuthOptions options)
        {
            // Veritabanı başlatma
            AuthFrameworkInitializer.Initialize(options.ConnectionString, options.CreateDatabaseIfNotExists);

            // Auth servisi oluştur ve döndür
            return AuthFrameworkInitializer.CreateAuthService(options.ConnectionString, options);
        }
    }
}
