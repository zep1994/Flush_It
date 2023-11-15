using Flush_It_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Flush_It_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<IbsCount> IbsCount => Set<IbsCount>();
        public DbSet<Food> Food => Set<Food>();

    }
}
