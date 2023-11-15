using Flush_It_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Flush_It_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<IbsCount> IbsCount => Set<IbsCount>();
        public DbSet<Food> Food => Set<Food>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Activity> Activities { get; set; }
        public DbSet<FoodActivity> FoodActivities => Set<FoodActivity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your relationships and constraints here

            modelBuilder.Entity<FoodActivity>()
                .HasKey(fa => new { fa.FoodId, fa.ActivityId });

            modelBuilder.Entity<FoodActivity>()
                .HasOne(fa => fa.Food)
                .WithMany(f => f.FoodActivities)
                .HasForeignKey(fa => fa.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodActivity>()
                .HasOne(fa => fa.Activity)
                .WithMany(a => a.FoodActivities)
                .HasForeignKey(fa => fa.ActivityId)
                .OnDelete(DeleteBehavior.Restrict); 


            base.OnModelCreating(modelBuilder);
        }
    }
}
