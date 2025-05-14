using Microsoft.EntityFrameworkCore;
using BaoDienTu_ASPNET.Models;

namespace BaoDienTu_ASPNET
{
    public class BaoDienTuDbContext : DbContext
    {
        public BaoDienTuDbContext(DbContextOptions<BaoDienTuDbContext> options) : base(options) { }

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
}
