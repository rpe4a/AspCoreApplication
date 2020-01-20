using System;
using EntitiesLib.Models;
using Microsoft.EntityFrameworkCore;

namespace EntitiesLib
{
    public class AppDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}