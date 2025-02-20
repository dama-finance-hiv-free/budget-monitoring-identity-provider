using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Data.Configuration
{
    public class SecurityQuestionConfiguration : IEntityTypeConfiguration<SecurityQuestion>
    {
        public void Configure(EntityTypeBuilder<SecurityQuestion> entity)
        {
            entity.HasKey(e => new { e.Code, e.Locale }).HasName("PK_SecurityQuestion");

            entity.Property(e => e.Code).HasMaxLength(2);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Locale).HasMaxLength(5);

            entity.ToTable("SecurityQuestions");
        }
    }
}