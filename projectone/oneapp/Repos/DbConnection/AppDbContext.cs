using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using oneapp.Entities;

namespace oneapp.Repos
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string,
                                     IdentityUserClaim<string>, IdentityUserRole<string>,
                                     IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<ImageHub> ImageHub { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var uName = "admin@example.com";

            // Create default admin user
            var adminUser = new IdentityUser
            {
                Id = "3e5e6b36-8fab-4aa3-91ad-025cce8a4151",
                UserName = uName,
                Email = uName,
                NormalizedEmail = uName.ToUpper(),
                NormalizedUserName = uName.ToUpper(),
                EmailConfirmed = true // Assuming the email is confirmed
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "root");

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            // Create roles
            var adminRole = new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var userRole = new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            };

            modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);

            // Assign roles to admin user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            });

            // Include IdentityRoleClaim<string>
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims"); // Define table name if needed
            });
        }
    }
}
