using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    /// <summary>
    /// Entity Framework configuration for the OperationClaim entity.
    /// Configures table name, primary key, properties, indexes, and relationships.
    /// </summary>
    public class OperationClaimConfiguration : EntityTypeConfiguration<OperationClaim>
    {
        /// <summary>
        /// Initializes the OperationClaim entity configuration.
        /// </summary>
        public OperationClaimConfiguration()
        {
            // Configure the primary key
            HasKey(o => o.Id);

            // Configure the Name property (required, max length 50, Unicode supported)
            Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            // Configure the optional Description property (max length 250)
            Property(o => o.Description)
                .HasMaxLength(250)
                .IsOptional();

            // Set the table name
            ToTable("OperationClaims");

            // Create a unique index on the Name column
            HasIndex(o => o.Name)
                .IsUnique(true)
                .HasName("IX_OperationClaim_Name");

            // Configure one-to-many relationship with UserOperationClaim
            HasMany(o => o.UserOperationClaims)
                .WithRequired(uoc => uoc.OperationClaim)
                .HasForeignKey(uoc => uoc.OperationClaimId);
        }
    }
}
