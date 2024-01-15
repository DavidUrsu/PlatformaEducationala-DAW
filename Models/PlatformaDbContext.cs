using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationala_DAW.Models
{

    public class PlatformaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public PlatformaDbContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}