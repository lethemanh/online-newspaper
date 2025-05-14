using BaoDienTu_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaoDienTu_ASPNET.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BaoDienTuDbContext _context;
        public CategoryController(BaoDienTuDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Error = "Category name is required.";
                return View();
            }
            if (await _context.Categories.AnyAsync(c => c.Name == name))
            {
                ViewBag.Error = "Category already exists.";
                return View();
            }
            var category = new Category { Name = name, Description = description };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name, string? description)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Error = "Category name is required.";
                return View(category);
            }
            if (await _context.Categories.AnyAsync(c => c.Name == name && c.Id != id))
            {
                ViewBag.Error = "Category already exists.";
                return View(category);
            }
            category.Name = name;
            category.Description = description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            var hasNews = await _context.News.AnyAsync(n => n.CategoryId == id);
            if (hasNews)
            {
                TempData["DeleteError"] = "Không thể xoá loại tin này vì đang có tin tức liên quan.";
                return RedirectToAction("Index");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
