using Microsoft.EntityFrameworkCore;

namespace Lab3CodeFirst.Models
{
    public class ComputerCoursesDbContext: DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Database=DB_ComputerCourses;Username=***;Password=***");
    }
}
