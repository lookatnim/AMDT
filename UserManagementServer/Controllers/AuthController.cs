using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.Helpers;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            // 1. Validate user credentials (Include RoleType and Status to avoid null reference)
            var user = _context.Users
                .Include(u => u.RoleType)
                .Include(u => u.Status)
                .FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            if (!PasswordHelper.VerifyPassword(model.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            // 2. Generate JWT Token
            var token = GenerateJwtToken(user.UserID, user.Email, user.RoleType?.RoleName ?? "User", user.Status?.StatusName ?? "Active");

            // 3. Return the token
            return Ok(new { token });
        }

        

        private string GenerateJwtToken(int userId, string email, string roleName, string statusName)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            if (string.IsNullOrEmpty(jwtSettings["SecretKey"]))
            {
                throw new Exception("JWT Secret Key is missing in appsettings.json.");
            }

            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("Status", statusName)
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpirationMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
