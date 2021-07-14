using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.EF.Configurations;
using WebAPI.EF.Entities;

namespace WebAPI.EF
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
          
            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
