using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LVCore.LVApp.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API yapılandırması ve servisleri

            // CORS desteği (isteğe bağlı)
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // OWIN JWT kimlik doğrulamasını etkinleştir
            config.SuppressDefaultHostAuthentication(); // IIS kimlik doğrulamasını devre dışı bırak
            config.Filters.Add(new HostAuthenticationFilter("Bearer")); // Bearer token kimlik doğrulamasını etkinleştir

            // Web API rotaları
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // JSON formatını ayarla (isteğe bağlı)
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
