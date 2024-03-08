using Microsoft.EntityFrameworkCore;
using oneapp.Entities;

namespace oneapp.Repos.DbConnection
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}

