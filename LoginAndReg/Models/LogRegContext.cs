using Microsoft.EntityFrameworkCore; 
namespace LoginAndReg.Models
{
    public class LogRegContext : DbContext
    {
        public LogRegContext(DbContextOptions options) : base(options) { }
            
        // "users" table is represented by this DbSet "Users"
        public DbSet<User> Users {get;set;}
    }
}