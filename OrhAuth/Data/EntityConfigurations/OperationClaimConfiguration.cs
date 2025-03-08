using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    public class OperationClaimConfiguration : EntityTypeConfiguration<OperationClaim>
    {
        public OperationClaimConfiguration()
        {
            // Primary Key
            HasKey(o => o.Id);

            // Özellikler
            Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);  // Unicode karakter desteği

            // İsteğe bağlı - Description alanı varsa
            Property(o => o.Description)
                .HasMaxLength(250)
                .IsOptional();

            // Tablo adı
            ToTable("OperationClaims");

            // Benzersiz indeks - aynı isimden bir daha oluşturulamasın
            HasIndex(o => o.Name)
                .IsUnique(true)
                .HasName("IX_OperationClaim_Name");

            // İlişki - UserOperationClaim ile
            HasMany(o => o.UserOperationClaims)
                .WithRequired(uoc => uoc.OperationClaim)
                .HasForeignKey(uoc => uoc.OperationClaimId);
        }
    }
}
