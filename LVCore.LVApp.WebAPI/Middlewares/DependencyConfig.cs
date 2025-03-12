using Autofac.Integration.WebApi;
using Autofac;
using LVCore.LVApp.BusinessService.Managers;
using LVCore.LVApp.BusinessService.Services;
using System.Reflection;
using System.Web.Http;
using LVCore.LVApp.DataAccessService.Repositories.Concrete;
using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.UoW;
using System.Configuration;
using OrhAuth.Extensions;
using OrhAuth.Services;
using OrhAuth.Configurations;
using LVCore.LVApp.Shared.ExtendEntities;
using LVCore.LVApp.BusinessService.Managers.AuthManagers;
using LVCore.LVApp.BusinessService.Services.AuthServices;

namespace LVCore.LVApp.WebAPI.Middlewares
{
    public static class DependencyConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // 📌 Web API controller'larını ekleyelim
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 📌 Unit of Work ve Database Connection yönetimi
            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerRequest(); // Her istek için yeni instance oluşur

            var authOptions = new OrhAuthOptions
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString,
                CreateDatabaseIfNotExists = true,
                TokenSecurityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"],
                TokenIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
                TokenAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
                TokenExpirationMinutes = int.Parse(ConfigurationManager.AppSettings["Jwt:AccessTokenExpiration"]),

                ExtendedUserType = typeof(ExtendedUser)
            };

            // Auth servisini yapılandır ve Autofac'e kaydet
            var authService = OrhAuthExtensions.ConfigureOrhAuth(authOptions);
            builder.RegisterInstance(authService).As<IAuthService>().SingleInstance();



            // 📌 Repository'leri ekleyelim
            builder.RegisterType<StockRepository>().As<IStockRepository>().InstancePerRequest();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerRequest();

            // 📌 Business Service'leri ekleyelim
            builder.RegisterType<StockBusinessManager>().As<IStockBusinessService>().InstancePerRequest();
            builder.RegisterType<LoginBusinessManager>().As<ILoginBusinessService>().InstancePerRequest();
            builder.RegisterType<AuthBusinessManager>().As<IAuthBusinessService>().InstancePerRequest();

            // 📌 Container'ı yapılandır ve Web API'ye uygula
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}