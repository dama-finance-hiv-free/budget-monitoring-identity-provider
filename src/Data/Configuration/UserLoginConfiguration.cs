using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Data.Configuration;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_UserLogin");

        entity.Property(e => e.Id).HasMaxLength(50);
        entity.Property(e => e.User).HasMaxLength(50);
        entity.Property(e => e.Device).HasMaxLength(250);
        entity.Property(e => e.Address).HasMaxLength(50);

        entity.ToTable("UserLogins");
    }
}