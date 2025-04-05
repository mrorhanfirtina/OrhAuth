namespace OrhAuth.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object representing the relationship between a user and an operation claim (role).
    /// Typically used during user registration or role assignment operations.
    /// </summary>
    public class UserOperationClaimDto
    {
        /// <summary>
        /// Gets or sets the identifier of the associated operation claim (role/permission).
        /// </summary>
        public int OperationClaimId { get; set; }
    }
}
