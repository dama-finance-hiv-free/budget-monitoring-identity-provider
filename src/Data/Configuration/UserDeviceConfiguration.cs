using IdentityProvider.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Data.Configuration;

public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(EntityTypeBuilder<UserDevice> entity)
    {
        entity.HasKey(e => new { UserId = e.User, e.Device }).HasName("PK_UserDevice");

        entity.Property(e => e.User).HasMaxLength(75);
        entity.Property(e => e.Device).HasMaxLength(250);
        entity.Property(e => e.LastIp).HasMaxLength(100);

        entity.ToTable("UserDevices");
    }
}