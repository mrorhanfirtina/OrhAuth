using OrhAuth.Models.Entities.Base;

namespace OrhAuth.Models.Entities
{
    /// <summary>
    /// Represents the association between a user and an operation claim (role/permission).
    /// Each entry links a specific user to a specific permission within the system.
    /// </summary>
    public class UserOperationClaim : EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated operation claim (role/permission).
        /// </summary>
        public int OperationClaimId { get; set; }

        /// <summary>
        /// Navigation property for the user assigned to the claim.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Navigation property for the operation claim (role/permission) assigned to the user.
        /// </summary>
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
