using BaoDienTu_ASPNET;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<BaoDienTuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Auto-create admin account if not exists
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<BaoDienTuDbContext>();
  if (!db.Users.Any(u => u.Role == "Admin"))
  {
    var admin = new BaoDienTu_ASPNET.Models.User
    {
      UserName = "admin",
      Email = "admin@localhost",
      PasswordHash = BaoDienTu_ASPNET.Controllers.AccountController.HashPassword("123456"),
      Role = "Admin"
    };
    db.Users.Add(admin);
    db.SaveChanges();
  }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
