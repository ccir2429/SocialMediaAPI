using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<User> UserContexts { get; set; }
    }
}