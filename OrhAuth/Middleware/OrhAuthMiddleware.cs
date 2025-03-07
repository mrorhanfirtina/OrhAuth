using Microsoft.Owin;
using OrhAuth.Services;
using System.Threading.Tasks;

namespace OrhAuth.Middleware
{
    public class OrhAuthMiddleware : OwinMiddleware
    {
        private readonly IAuthService _authService;

        public OrhAuthMiddleware(OwinMiddleware next, IAuthService authService)
            : base(next)
        {
            _authService = authService;
        }

        public override async Task Invoke(IOwinContext context)
        {
            // Token doğrulama mantığı burada
            // ...

            await Next.Invoke(context);
        }
    }
}
