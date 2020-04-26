using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Models
{
    public class BatikContext : DbContext
    {
        public DbSet<Batik> Batiks { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<User> Users { get; set; }

        public BatikContext()
        {

        }
    }
}
