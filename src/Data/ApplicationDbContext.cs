using IdentityProvider.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        public virtual DbSet<MultiFactorType> MultiFactorTypeSet { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestionSet { get; set; }
        public virtual DbSet<UserSecurityQuestion> UserSecurityQuestionSet { get; set; }
        public virtual DbSet<UserMultiFactor> UserMultiFactorSet { get; set; }
        public virtual DbSet<UserDevice> UserDeviceSet { get; set; }
        public virtual DbSet<UserLogin> UserLoginSet { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(75);
                entity.Property(e => e.Organization).HasMaxLength(5);
                entity.Property(e => e.Locale).HasMaxLength(5);
                entity.Property(e => e.UserCode).HasMaxLength(10);
                entity.Property(e => e.ImageUrl).HasMaxLength(200);
            });

            builder.AddConfigurations();
        }
    }
}