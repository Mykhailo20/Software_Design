namespace Lab4.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}
