using CW_COMP_1471.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_COMP_1471
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Pricing> Pricings { get; set; }

        public DbSet<Play> Plays { get; set; }

        public DbSet<Role> Role { get; set; }

    }
}
