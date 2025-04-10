using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    /// <summary>
    /// Entity Framework configuration for the UserOperationClaim entity.
    /// Defines relationships between users and their assigned roles (operation claims).
    /// </summary>
    public class UserOperationClaimConfiguration : EntityTypeConfiguration<UserOperationClaim>
    {
        /// <summary>
        /// Initializes the UserOperationClaim entity configuration.
        /// </summary>
        public UserOperationClaimConfiguration()
        {
            // Primary Key
            HasKey(uoc => uoc.Id);

            // Set table name
            ToTable("UserOperationClaims");

            // Relationship with User entity
            HasRequired(uoc => uoc.User)
                .WithMany(u => u.UserOperationClaims)
                .HasForeignKey(uoc => uoc.UserId)
                .WillCascadeOnDelete(false); // Prevent deletion of claims when user is deleted

            // Relationship with OperationClaim entity
            HasRequired(uoc => uoc.OperationClaim)
                .WithMany(oc => oc.UserOperationClaims)
                .HasForeignKey(uoc => uoc.OperationClaimId)
                .WillCascadeOnDelete(false); // Prevent deletion of mappings when role is deleted

            // Unique index to prevent assigning the same role to a user multiple times
            HasIndex(uoc => new { uoc.UserId, uoc.OperationClaimId })
                .IsUnique(true)
                .HasName("IX_UserOperationClaim_UserIdOperationClaimId");
        }
    }
}
