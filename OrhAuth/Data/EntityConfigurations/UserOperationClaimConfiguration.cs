using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    public class UserOperationClaimConfiguration : EntityTypeConfiguration<UserOperationClaim>
    {
        public UserOperationClaimConfiguration()
        {
            // Primary Key
            HasKey(uoc => uoc.Id);

            // Tablo adı
            ToTable("UserOperationClaims");

            // İlişkiler - User ile ilişki
            HasRequired(uoc => uoc.User)
                .WithMany(u => u.UserOperationClaims)
                .HasForeignKey(uoc => uoc.UserId)
                .WillCascadeOnDelete(false); // Kullanıcı silinirse, izinleri de silinmesin

            // İlişkiler - OperationClaim ile ilişki
            HasRequired(uoc => uoc.OperationClaim)
                .WithMany(oc => oc.UserOperationClaims)
                .HasForeignKey(uoc => uoc.OperationClaimId)
                .WillCascadeOnDelete(false); // Rol silinirse, kullanıcı rol bağlantıları da silinmesin

            // Benzersiz indeks - her kullanıcıya aynı rol yalnızca bir kez atanabilir
            HasIndex(uoc => new { uoc.UserId, uoc.OperationClaimId })
                .IsUnique(true)
                .HasName("IX_UserOperationClaim_UserIdOperationClaimId");
        }
    }
}
