﻿using EntitiesLib.Models;
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

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}