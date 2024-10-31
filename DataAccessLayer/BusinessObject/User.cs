using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.BusinessObject
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.Manager;
    }
    public enum UserRole
    {
        Admin,
        Manager
    }
}
