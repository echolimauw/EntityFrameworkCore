using Microsoft.EntityFrameworkCore; 
namespace CRUDelicious.Models
{
    public class CRUDContext : DbContext
    {
        public CRUDContext(DbContextOptions options) : base(options) { }
            
        // "users" table is represented by this DbSet "Users"
        public DbSet<Dish> Dishes {get;set;}
    }
}