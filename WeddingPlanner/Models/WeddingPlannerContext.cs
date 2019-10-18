using Microsoft.EntityFrameworkCore; 
namespace WeddingPlanner.Models
{
    public class WeddingPlannerContext : DbContext
    {
        public WeddingPlannerContext(DbContextOptions options) : base(options) { }
            
        // "users" table is represented by this DbSet "Users"
        public DbSet<User> Users {get;set;}
    }
}