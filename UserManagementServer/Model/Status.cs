using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required, MaxLength(50)]
        public string StatusName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
