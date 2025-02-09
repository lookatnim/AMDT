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
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ POST: Add a new status
        [HttpPost("add")]
        public IActionResult AddStatus([FromBody] Status model)
        {
            if (_context.Statuses.Any(s => s.StatusName == model.StatusName))
            {
                return BadRequest("Status already exists.");
            }

            var status = new Status
            {
                StatusName = model.StatusName,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            _context.Statuses.Add(status);
            _context.SaveChanges();

            return Created("Status added successfully", status);
        }

        // ✅ GET: Get all statuses
        [HttpGet("getall")]
        public IActionResult GetAllStatuses()
        {
            var statuses = _context.Statuses.ToList();
            return Ok(statuses);
        }

        // ✅ PUT: Update a status
        [HttpPut("update/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] Status model)
        {
            var status = _context.Statuses.FirstOrDefault(s => s.StatusID == id);
            if (status == null)
            {
                return NotFound("Status not found.");
            }

            status.StatusName = model.StatusName ?? status.StatusName;
            status.ModifiedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return Ok("Status updated successfully.");
        }

        // ✅ DELETE: Delete a status
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStatus(int id)
        {
            var status = _context.Statuses.FirstOrDefault(s => s.StatusID == id);
            if (status == null)
            {
                return NotFound("Status not found.");
            }

            _context.Statuses.Remove(status);
            _context.SaveChanges();

            return Ok("Status deleted successfully.");
        }
    }
}
