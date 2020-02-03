using EntitiesLib.Models;
using Microsoft.EntityFrameworkCore;
using SampleApp.Models;

namespace EntitiesLib
{
    public class AppDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            var adminUser = new AuthUser() { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<AuthUser>().HasData(new AuthUser[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}