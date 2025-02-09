using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System;

namespace UserManagementServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        ValidAudience = Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]))
                    };
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed the database with the admin user if it's empty
            SeedDatabase(context);
        }

        // Seed database with admin user if tables are empty
        private void SeedDatabase(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                // Seed Roles
                context.Roles.Add(new RoleType { RoleID = 1, RoleName = "Admin", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow });
                context.Roles.Add(new RoleType { RoleID = 2, RoleName = "User", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow });
                context.SaveChanges();
            }

            if (!context.Statuses.Any())
            {
                // Seed Statuses
                context.Statuses.Add(new Status { StatusID = 1, StatusName = "Active", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow });
                context.SaveChanges();
            }

            if (!context.Users.Any()) // If no users exist, create the admin user
            {
                // Hash the password
                var hashedPassword = HashPassword("Admin123"); // Use a hashing function for security

                // Add the admin user
                context.Users.Add(new UserDetails
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    Password = hashedPassword,  // Store the hashed password
                    DateOfBirth = "1990-01-01",
                    RoleTypeID = 1, // Admin role
                    StatusID = 1,   // Active status
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                });

                context.SaveChanges();
            }
        }

        // Hashing function for password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes); // Return the hashed password
            }
        }
    }
}
