using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Configuration;
using System.Text;
using System.Web.Http;

[assembly: OwinStartup(typeof(LVCore.LVApp.WebAPI.Startup))]

namespace LVCore.LVApp.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Web API yapılandırmasını al
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // CORS yapılandırması (isteğe bağlı)
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // JWT kimlik doğrulama ayarları
            var securityKey = ConfigurationManager.AppSettings["Jwt:SecurityKey"];
            var issuer = ConfigurationManager.AppSettings["Jwt:Issuer"];
            var audience = ConfigurationManager.AppSettings["Jwt:Audience"];

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                    }
                });

            // Web API middleware'ini uygula
            app.UseWebApi(config);
        }
    }
}