using Day3.Models;
using Microsoft.EntityFrameworkCore;

namespace Day3.Repositories
{
    public class AppDbContext:DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        protected readonly DbContextOptions Configuration;
        public AppDbContext(DbContextOptions configuration) : base(configuration)
        {
            Configuration = configuration;
        }

    }
}
