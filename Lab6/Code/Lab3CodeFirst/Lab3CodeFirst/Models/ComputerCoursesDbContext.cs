using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3CodeFirst.Models
{
    public class ComputerCoursesDbContext: DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Database=DB_ComputerCourses;Username=***;Password=***");
    }
}
