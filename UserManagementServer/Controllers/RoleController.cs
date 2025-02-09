using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires JWT authentication for all endpoints
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        //POST: Add a new role
        [HttpPost("add")]
        [Authorize]
        public IActionResult AddRole([FromBody] RoleType model)
        {
            if (_context.Roles.Any(r => r.RoleName == model.RoleName))
            {
                return BadRequest("Role already exists.");
            }

            var role = new RoleType
            {
                RoleName = model.RoleName,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return Created("Role added successfully", role);
        }

        //GET: Get all roles
        [HttpGet("getall")]
        [Authorize]
        public IActionResult GetAllRoles()
        {
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        //  PUT: Update a role
        [HttpPut("update/{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleType model)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleID == id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            role.RoleName = model.RoleName ?? role.RoleName;
            role.ModifiedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return Ok("Role updated successfully.");
        }

        // ✅ DELETE: Delete a role
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleID == id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return Ok("Role deleted successfully.");
        }
    }
}
