using CityAppREST.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace CityAppREST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>();
            modelBuilder.Entity<Visitor>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
