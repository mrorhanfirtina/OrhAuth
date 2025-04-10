using OrhAuth.Configurations;
using OrhAuth.Services;

namespace OrhAuth.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and initialize the OrhAuth authentication system.
    /// </summary>
    public static class OrhAuthExtensions
    {
        /// <summary>
        /// Configures and initializes the OrhAuth authentication system for .NET Framework projects.
        /// This method sets up the database, applies schema extensions if provided, 
        /// and returns a fully configured authentication service instance.
        /// </summary>
        /// <param name="options">The configuration options for OrhAuth.</param>
        /// <returns>An <see cref="IAuthService"/> implementation for authentication and authorization operations.</returns>
        public static IAuthService ConfigureOrhAuth(OrhAuthOptions options)
        {
            // Initialize the database and apply schema extensions
            AuthFrameworkInitializer.Initialize(
                options.ConnectionString,
                options.CreateDatabaseIfNotExists,
                options.ExtendedUserType // Pass ExtendedUserType if provided
            );

            // Create and return the AuthService instance
            return AuthFrameworkInitializer.CreateAuthService(options.ConnectionString, options);
        }
    }
}
