using BaoDienTu_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaoDienTu_ASPNET.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        // Trang chủ - danh sách tin đã duyệt
        public async Task<IActionResult> Index()
        {
            var news = await _context.News.Include(n => n.Category).Include(n => n.Author)
                .Where(n => n.IsApproved)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return View(news);
        }

        // Xem chi tiết tin
        public async Task<IActionResult> Details(int id)
        {
            var news = await _context.News.Include(n => n.Category).Include(n => n.Author)
                .FirstOrDefaultAsync(n => n.Id == id && n.IsApproved);
            if (news == null) return NotFound();
            return View(news);
        }

        // Đăng tin (user)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string content, int categoryId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var news = new News
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                AuthorId = userId.Value,
                IsApproved = false,
                CreatedAt = DateTime.UtcNow
            };
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyNews");
        }

        // Danh sách tin của user
        public async Task<IActionResult> MyNews()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var news = await _context.News.Include(n => n.Category)
                .Where(n => n.AuthorId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return View(news);
        }

        // Danh sách tin chờ duyệt (admin)
        public async Task<IActionResult> Pending()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return NotFound();
            var news = await _context.News.Include(n => n.Category).Include(n => n.Author)
                .Where(n => !n.IsApproved)
                .OrderBy(n => n.CreatedAt)
                .ToListAsync();
            return View(news);
        }

        // Duyệt tin (admin)
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return Unauthorized();
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            news.IsApproved = true;
            news.ApprovedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return RedirectToAction("Pending");
        }
    }
}
