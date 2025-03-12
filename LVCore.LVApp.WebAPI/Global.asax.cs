using LVCore.LVApp.BusinessService.AutoMapper;
using LVCore.LVApp.WebAPI.Middlewares;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace LVCore.LVApp.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyConfig.Register(); // 📌 Autofac DI Middleware'i başlat!    
            AutoMapperConfig.RegisterMappings();// 📌 AutoMapper başlatılıyor
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //SwaggerConfig.Register();
        }
    }
}
