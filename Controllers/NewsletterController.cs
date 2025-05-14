using BaoDienTu_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaoDienTu_ASPNET.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly AppDbContext _context;
        public NewsletterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Email is required.";
                return View();
            }
            if (await _context.NewsletterSubscriptions.AnyAsync(n => n.Email == email))
            {
                ViewBag.Error = "This email is already subscribed.";
                return View();
            }
            var sub = new NewsletterSubscription { Email = email };
            _context.NewsletterSubscriptions.Add(sub);
            await _context.SaveChangesAsync();
            ViewBag.Success = "Subscribed successfully!";
            return View();
        }
    }
}
