using System.ComponentModel.DataAnnotations;

namespace BaoDienTu_ASPNET.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
