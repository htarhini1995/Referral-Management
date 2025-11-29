using FleetManagement.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.EF
{
    public class ReferralDbContext : DbContext
    {
        public static IDbContextFactory<ReferralDbContext>? Factory { get; private set; }
        public static void ConfigureFactory(IDbContextFactory<ReferralDbContext> factory) => Factory = factory;

        public ReferralDbContext(DbContextOptions<ReferralDbContext> o) : base(o)
        {

        }
        public DbSet<UserLogin> UserLogin => Set<UserLogin>();
        public  DbSet<StoredDocument> StoredDocuments => Set<StoredDocument>();
        public  DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.HasDefaultSchema("public");

            b.Entity<UserLogin>(e =>
            {
                e.ToTable("UserLogin");       
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityByDefaultColumn();

                e.Property(x => x.Email).HasMaxLength(255).IsRequired();
                e.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();

                e.Property(x => x.Username)
                 .HasColumnName("Username")   
                 .HasMaxLength(255)
                 .IsRequired();

                e.HasIndex(x => x.Email).IsUnique();
            });
        }
    }
}
