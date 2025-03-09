using OrhAuth.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace OrhAuth.Data.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(u => u.Id);

            // String alanlar için optimum uzunluklar belirleme
            Property(u => u.Email).IsRequired().HasMaxLength(100);
            Property(u => u.FirstName).HasMaxLength(50);
            Property(u => u.LastName).HasMaxLength(50);
            Property(u => u.Username).HasMaxLength(50);

            // ŞİFRE SIFIRLAMA ALANLARI EKLENMELİ
            Property(u => u.PasswordResetToken).HasMaxLength(100).IsOptional();
            Property(u => u.PasswordResetTokenSalt).HasMaxLength(100).IsOptional();
            Property(u => u.PasswordResetTokenExpiry).IsOptional();

            // İlişkiler
            HasMany(u => u.UserOperationClaims)
                .WithRequired(uoc => uoc.User)
                .HasForeignKey(uoc => uoc.UserId);

            HasMany(u => u.RefreshTokens)
                .WithRequired(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}
