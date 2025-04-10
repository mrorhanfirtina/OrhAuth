using System.Collections.Generic;

namespace OrhAuth.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object used for user registration operations.
    /// Contains user identity information and optional role claims.
    /// </summary>
    public class UserForRegisterDto
    {
        /// <summary>
        /// Gets or sets the user's email address used for login and identification.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password in plain text. It will be hashed before storage.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's locality or geographical identifier (optional).
        /// </summary>
        public string LocalityId { get; set; }

        /// <summary>
        /// Gets or sets the list of role claims (OperationClaims) to be assigned to the user during registration.
        /// </summary>
        public ICollection<UserOperationClaimDto> UserOperationClaims { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserForRegisterDto"/> class with an empty role claim list.
        /// </summary>
        public UserForRegisterDto()
        {
            UserOperationClaims = new List<UserOperationClaimDto>();
        }
    }
}
