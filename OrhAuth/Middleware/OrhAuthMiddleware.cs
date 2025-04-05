using Microsoft.Owin;
using OrhAuth.Services;
using System.Threading.Tasks;

namespace OrhAuth.Middleware
{
    /// <summary>
    /// Represents a custom OWIN middleware for handling token validation and authentication using OrhAuth.
    /// This middleware can be used to process incoming requests and perform custom logic such as JWT verification.
    /// </summary>
    public class OrhAuthMiddleware : OwinMiddleware
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrhAuthMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the OWIN pipeline.</param>
        /// <param name="authService">An instance of <see cref="IAuthService"/> used for authentication operations.</param>
        public OrhAuthMiddleware(OwinMiddleware next, IAuthService authService)
            : base(next)
        {
            _authService = authService;
        }

        /// <summary>
        /// Processes an individual OWIN request to perform token validation and other custom authentication logic.
        /// </summary>
        /// <param name="context">The current OWIN context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task Invoke(IOwinContext context)
        {
            // Token validation logic can be placed here if custom processing is needed.
            // You may access and validate the token from the Authorization header.

            await Next.Invoke(context);
        }
    }
}
