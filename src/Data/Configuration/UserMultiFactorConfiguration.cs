using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Data.Configuration
{
    public class UserMultiFactorConfiguration : IEntityTypeConfiguration<UserMultiFactor>
    {
        public void Configure(EntityTypeBuilder<UserMultiFactor> entity)
        {
            entity.HasKey(e => new { e.User, e.MultiFactorType }).HasName("PK_UserMultiFactor");

            entity.Property(e => e.User).HasMaxLength(75);
            entity.Property(e => e.MultiFactorType).HasMaxLength(2);

            entity.ToTable("UserMultiFactors");
        }
    }
}