using Microsoft.EntityFrameworkCore;
using DevLife.APIproj.Models;

namespace DevLife.APIproj.Data
{
    public class DevLifeDbContext : DbContext
    {
        public DevLifeDbContext(DbContextOptions<DevLifeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }



    }
}
