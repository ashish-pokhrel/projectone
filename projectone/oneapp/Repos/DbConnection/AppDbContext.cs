using Microsoft.EntityFrameworkCore;
using oneapp.Entities;

namespace oneapp.Repos.DbConnection
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<ImageHub> ImageHub { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}

