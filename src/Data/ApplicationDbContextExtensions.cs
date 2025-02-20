using IdentityProvider.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data
{
    public static class ApplicationDbContextExtensions
    {
        public static void AddConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MultiFactorTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SecurityQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new UserSecurityQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new UserMultiFactorConfiguration());
            modelBuilder.ApplyConfiguration(new UserDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        }
    }
}