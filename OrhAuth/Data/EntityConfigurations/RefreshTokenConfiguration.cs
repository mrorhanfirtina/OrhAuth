using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    /// <summary>
    /// Entity Framework configuration for the RefreshToken entity.
    /// Defines table schema, property constraints, indexes, and relationships.
    /// </summary>
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        /// <summary>
        /// Initializes the RefreshToken entity configuration.
        /// </summary>
        public RefreshTokenConfiguration()
        {
            // Configure the primary key
            HasKey(rt => rt.Id);

            // Set the table name
            ToTable("RefreshTokens");

            // Configure properties
            Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(500); // Sufficient length for token storage

            Property(rt => rt.Expires)
                .IsRequired();

            Property(rt => rt.CreatedByIp)
                .HasMaxLength(50);

            Property(rt => rt.Revoked)
                .IsOptional();

            Property(rt => rt.RevokedByIp)
                .HasMaxLength(50)
                .IsOptional();

            Property(rt => rt.ReplacedByToken)
                .HasMaxLength(500)
                .IsOptional();

            // Configure relationships
            HasRequired(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .WillCascadeOnDelete(true); // Cascade delete when the user is deleted

            // Create a unique index on the Token property for quick validation
            HasIndex(rt => rt.Token)
                .IsUnique(true)
                .HasName("IX_RefreshToken_Token");

            // Create a composite index on UserId and Expires for performance
            HasIndex(rt => new { rt.UserId, rt.Expires })
                .HasName("IX_RefreshToken_UserIdExpires");
        }
    }
}
