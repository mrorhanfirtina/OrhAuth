using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    /// <summary>
    /// Entity Framework configuration for the User entity.
    /// Defines property constraints, relationships, and schema mappings.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes the User entity configuration.
        /// </summary>
        public UserConfiguration()
        {
            // Primary key
            HasKey(u => u.Id);

            // Define property constraints for string fields
            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.FirstName)
                .HasMaxLength(50);

            Property(u => u.LastName)
                .HasMaxLength(50);

            Property(u => u.Username)
                .HasMaxLength(50);

            // Password reset fields
            Property(u => u.PasswordResetToken)
                .HasMaxLength(100)
                .IsOptional();

            Property(u => u.PasswordResetTokenSalt)
                .HasMaxLength(100)
                .IsOptional();

            Property(u => u.PasswordResetTokenExpiry)
                .IsOptional();

            // Relationships

            // A user can have many user-role mappings
            HasMany(u => u.UserOperationClaims)
                .WithRequired(uoc => uoc.User)
                .HasForeignKey(uoc => uoc.UserId);

            // A user can have many refresh tokens
            HasMany(u => u.RefreshTokens)
                .WithRequired(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}
