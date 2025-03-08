using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration()
        {
            // Primary Key
            HasKey(rt => rt.Id);

            // Tablo adı
            ToTable("RefreshTokens");

            // Özellikler
            Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(500); // Token uzunluğu için yeterli

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

            // İlişkiler
            HasRequired(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .WillCascadeOnDelete(true); // Kullanıcı silindiğinde, refresh token'ları da silinsin

            // Token için indeks - doğrulama sırasında hızlı erişim için
            HasIndex(rt => rt.Token)
                .IsUnique(true)
                .HasName("IX_RefreshToken_Token");

            // UserId ve Expires için bileşik indeks - aktif token'ları sorgularken performans için
            HasIndex(rt => new { rt.UserId, rt.Expires })
                .HasName("IX_RefreshToken_UserIdExpires");
        }
    }
}
