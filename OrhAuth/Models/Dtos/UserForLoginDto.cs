namespace OrhAuth.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object used for user login operations.
    /// Contains the credentials required to authenticate a user.
    /// </summary>
    public class UserForLoginDto
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting to log in.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the plaintext password provided by the user during login.
        /// </summary>
        public string Password { get; set; }
    }
}
