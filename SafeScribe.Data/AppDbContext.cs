using Microsoft.EntityFrameworkCore;
using SafeScribe.Model;

namespace SafeScribe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }

    }
}
