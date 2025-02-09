﻿    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using UserManagementAPI.Data;
    using UserManagementAPI.Models;
    using UserManagementAPI.Helpers;
    using System;
    using Microsoft.EntityFrameworkCore;

    namespace UserManagementAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public UserController(ApplicationDbContext context)
            {
                _context = context;
            }

            // Protect this API method with JWT authentication
            // GET: api/user/getall
            [HttpGet("getall")]
            [Authorize] // Requires a valid JWT token
            public IActionResult GetAllUsers()
            {
                var users = _context.Users.ToList();
                return Ok(users);
            }

            [HttpGet("getbyemail")]
            [Authorize]
            public IActionResult GetUserByEmail(string email)
            {
                var user = _context.Users
                    .Include(u => u.RoleType)
                    .Include(u => u.Status)
                    .Where(u => u.Email == email)
                    .Select(u => new
                    {
                        u.UserID,
                        u.FirstName,
                        u.LastName,
                        u.Email,
                        Role = u.RoleType.RoleName,
                        Status = u.Status.StatusName,
                        u.CreatedAt,
                        u.ModifiedAt
                    })
                    .FirstOrDefault();

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }

            [HttpPut("update/{id}")]
            [Authorize]
            public IActionResult UpdateUser(int id, [FromBody] CommonUser model)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserID == id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Update user details
                user.FirstName = model.FirstName ?? user.FirstName;
                user.LastName = model.LastName ?? user.LastName;
                user.Email = model.Email ?? user.Email;
                user.RoleTypeID = model.RoleTypeID != 0 ? model.RoleTypeID : user.RoleTypeID;
                user.StatusID = model.StatusID != 0 ? model.StatusID : user.StatusID;
                user.ModifiedAt = DateTime.UtcNow;

                _context.SaveChanges();

                return Ok(user);
            }

            [HttpDelete("delete/{id}")]
            [Authorize]
            public IActionResult DeleteUser(int id)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserID == id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                return Ok("User deleted successfully.");
            }

            [HttpPost("add")]
            [Authorize]
            public IActionResult AddUser([FromBody] UserDetails model)
            {
                // Validate if the email already exists
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    return BadRequest("Email is already in use.");
                }

                // Encrypt password using SHA256
                string hashedPassword = PasswordHelper.HashPassword(model.Password);

                // Create a new user
                var user = new UserDetails
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = hashedPassword, // Store the encrypted password
                    DateOfBirth = model.DateOfBirth,
                    RoleTypeID = model.RoleTypeID, // Assume RoleTypeID is provided
                    StatusID = model.StatusID, // Assume StatusID is provided
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return Created("User added successfully", user);
            }

        }
    }
