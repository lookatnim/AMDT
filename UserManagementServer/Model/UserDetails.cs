using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models
{
    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Will store hashed password

        public string DateOfBirth { get; set; }

        [ForeignKey("RoleType")]
        public int RoleTypeID { get; set; }
        public RoleType RoleType { get; set; }

        [ForeignKey("Status")]
        public int StatusID { get; set; }
        public Status Status { get; set; }
        public string VerificationToken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
