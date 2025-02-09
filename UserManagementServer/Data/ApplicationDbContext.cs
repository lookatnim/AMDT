using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using UserManagementAPI.Models;

namespace UserManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<RoleType> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }

        // Seed data if tables are empty
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed RoleType table (create "Admin" role)
            modelBuilder.Entity<RoleType>().HasData(
                new RoleType { RoleID = 1, RoleName = "Admin", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow }
            );

            // Seed Status table (create "Active" status)
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusID = 1, StatusName = "Active", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
                new Status { StatusID = 2, StatusName = "Deactive", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow }
            );
        }
    }
}
