using CityAppREST.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace CityAppREST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
