using Microsoft.EntityFrameworkCore;

namespace tireFitting.Models
{
    public class TireFittingContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<People> Peoples { get; set; }

        public TireFittingContext(DbContextOptions<TireFittingContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
