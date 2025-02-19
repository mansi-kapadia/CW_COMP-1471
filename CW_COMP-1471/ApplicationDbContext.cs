using Microsoft.EntityFrameworkCore;

namespace CW_COMP_1471
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //public DbSet<User> Users { get; set; }  // Example DbSet
    }
}
