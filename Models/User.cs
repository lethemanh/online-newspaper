using System.ComponentModel.DataAnnotations;

namespace BaoDienTu_ASPNET.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; } // "User" hoặc "Admin"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
