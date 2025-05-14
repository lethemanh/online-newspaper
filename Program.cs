using BaoDienTu_ASPNET;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

// Load environment variables from .env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Read DB provider and connection string from env
var dbProvider = Environment.GetEnvironmentVariable("DB_PROVIDER") ?? "SqlServer";
string? connStr = null;
if (dbProvider == "Sqlite")
    connStr = Environment.GetEnvironmentVariable("SQLITE_CONNECTION_STRING") ?? "Data Source=BaoDienTu.db";
else
    connStr = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? "Server=localhost;Database=BaoDienTu;User Id=sa;Password=YourStrong!Passw0rd;";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<BaoDienTuDbContext>(options =>
{
    if (dbProvider == "Sqlite")
        options.UseSqlite(connStr);
    else
        options.UseSqlServer(connStr);
});

// Set ASP.NET Core port from env
var aspnetUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (!string.IsNullOrEmpty(aspnetUrls))
    builder.WebHost.UseUrls(aspnetUrls);

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
