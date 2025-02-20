using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Data.Configuration
{
    public class MultiFactorTypeConfiguration : IEntityTypeConfiguration<MultiFactorType>
    {
        public void Configure(EntityTypeBuilder<MultiFactorType> entity)
        {
            entity.HasKey(e => new { e.Code, e.Locale }).HasName("PK_MultiFactorType");

            entity.Property(e => e.Code).HasMaxLength(2);
            entity.Property(e => e.Description).HasMaxLength(25);
            entity.Property(e => e.Locale).HasMaxLength(5);

            entity.ToTable("MultiFactorTypes");
        }
    }
}