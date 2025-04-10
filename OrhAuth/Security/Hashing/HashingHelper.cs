using System.Security.Cryptography;
using System.Text;

namespace OrhAuth.Security.Hashing
{
    /// <summary>
    /// Provides helper methods for securely hashing and verifying passwords using HMACSHA512.
    /// </summary>
    public static class HashingHelper
    {
        /// <summary>
        /// Generates a password hash and salt using the HMACSHA512 algorithm.
        /// </summary>
        /// <param name="password">The plain text password to be hashed.</param>
        /// <param name="passwordHash">The resulting hashed password as a byte array.</param>
        /// <param name="passwordSalt">The generated salt used in the hashing process.</param>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verifies whether a given plain text password matches the provided hash and salt.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="passwordHash">The original hashed password.</param>
        /// <param name="passwordSalt">The salt used when hashing the original password.</param>
        /// <returns>True if the password matches the hash; otherwise, false.</returns>
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
