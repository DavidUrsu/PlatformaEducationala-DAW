using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationala_DAW.Models
{

    public class PlatformaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        public PlatformaDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(u => u.TaughtCourses)
                .HasForeignKey(c => c.ProfessorUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}