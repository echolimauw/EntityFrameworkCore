using Microsoft.EntityFrameworkCore; 
namespace ActivityCenter.Models
{
    public class ACContext : DbContext
    {
        public ACContext(DbContextOptions options) : base(options) { }
            
        // "users" table is represented by this DbSet "Users"
        public DbSet<User> Users {get;set;}  // We're gonna say this is due to migrations not being there? Correct as needed.     
        public DbSet<Plan> Plans {get; set;}
    }
}