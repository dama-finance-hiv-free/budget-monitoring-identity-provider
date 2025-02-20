using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Data.Configuration
{
    public class UserSecurityQuestionConfiguration : IEntityTypeConfiguration<UserSecurityQuestion>
    {
        public void Configure(EntityTypeBuilder<UserSecurityQuestion> entity)
        {
            entity.HasKey(e => new { e.User, e.Question }).HasName("PK_UserSecurityQuestion");

            entity.Property(e => e.User).HasMaxLength(75);
            entity.Property(e => e.Question).HasMaxLength(2);
            entity.Property(e => e.Answer).HasMaxLength(100);

            entity.ToTable("UserSecurityQuestions");
        }
    }
}