using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Models
{
    public class BatikContext : DbContext
    {
        public DbSet<Batik> Batiks { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<User> Users { get; set; }

        public BatikContext(DbContextOptions<BatikContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Locality>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Username = "admin",
                Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg="
            });
        }
    }
}
