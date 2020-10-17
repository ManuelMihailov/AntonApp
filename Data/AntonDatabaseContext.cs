using Data.Configuration;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class AntonDatabaseContext : DbContext
    {
        public AntonDatabaseContext()
        {
        }

        public AntonDatabaseContext(DbContextOptions<AntonDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Request> Requests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=AntonApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
