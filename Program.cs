using BaoDienTu_ASPNET;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

// Load environment variables from .env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Read DB provider and connection string from env
var config = builder.Configuration;
var dbProvider = config["DB_PROVIDER"] ?? "SqlServer";
string? connStr = null;
if (dbProvider == "Sqlite")
  connStr = config.GetConnectionString("Sqlite") ?? "Data Source=BaoDienTu.db";
else
  connStr = config.GetConnectionString("SqlServer") ?? "Server=localhost;Database=BaoDienTu;User Id=sa;Password=YourStrong!Passw0rd;";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
if (dbProvider.ToUpper() == "SQLITE")
{
  builder.Services.AddDbContext<SqliteDbContext>(options =>
      options.UseSqlite(connStr));
  builder.Services.AddScoped<AppDbContext>(sp => sp.GetRequiredService<SqliteDbContext>());
}
else
{
  builder.Services.AddDbContext<SqlServerDbContext>(options =>
      options.UseSqlServer(connStr));
  builder.Services.AddScoped<AppDbContext>(sp => sp.GetRequiredService<SqlServerDbContext>());
}
// To use: inject AppDbContext in controllers/services

// Set ASP.NET Core port from env
var aspnetUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (!string.IsNullOrEmpty(aspnetUrls))
  builder.WebHost.UseUrls(aspnetUrls);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

  // Tạo tài khoản admin nếu chưa có
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
