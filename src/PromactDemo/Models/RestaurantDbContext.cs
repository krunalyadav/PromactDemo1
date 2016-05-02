using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace PromactDemo.Models
{
    public class RestaurantDbContext : IdentityDbContext<User>
    {
        public DbSet<Restaurant> Restaurant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RestaurantDb;Trusted_Connection=True;");
        }
    }
}