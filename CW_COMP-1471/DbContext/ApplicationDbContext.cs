using CW_COMP_1471.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_COMP_1471
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<PricingBand> Pricings { get; set; }

        public DbSet<Play> Plays { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
