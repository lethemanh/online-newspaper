using Microsoft.EntityFrameworkCore;
using BaoDienTu_ASPNET.Models;

// Base DbContext
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<NewsRelation> NewsRelations { get; set; }
    public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
    public DbSet<NewsComment> NewsComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewsRelation>()
            .HasOne(nr => nr.News)
            .WithMany(n => n.RelatedNews)
            .HasForeignKey(nr => nr.NewsId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NewsRelation>()
            .HasOne(nr => nr.RelatedNews)
            .WithMany()
            .HasForeignKey(nr => nr.RelatedNewsId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<NewsComment>()
            .HasOne(nc => nc.News)
            .WithMany(n => n.Comments)
            .HasForeignKey(nc => nc.NewsId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<NewsComment>()
            .HasOne(nc => nc.User)
            .WithMany()
            .HasForeignKey(nc => nc.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

// SQL Server context
public class SqlServerDbContext : AppDbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options) { }
}

// SQLite context
public class SqliteDbContext : AppDbContext
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
        : base(options) { }
}
