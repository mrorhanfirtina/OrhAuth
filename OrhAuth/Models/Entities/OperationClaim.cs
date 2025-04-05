using OrhAuth.Models.Entities.Base;
using System.Collections.Generic;

namespace OrhAuth.Models.Entities
{
    /// <summary>
    /// Represents a role or permission definition within the system.
    /// Operation claims are used to define what actions a user is authorized to perform.
    /// </summary>
    public class OperationClaim : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique name of the operation claim (e.g., "Admin", "User").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an optional description for the operation claim.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Navigation property for user-role associations.
        /// </summary>
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationClaim"/> class.
        /// </summary>
        public OperationClaim()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }
    }
}
