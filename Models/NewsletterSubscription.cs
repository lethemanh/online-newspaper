using System.ComponentModel.DataAnnotations;

namespace BaoDienTu_ASPNET.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    }
}
