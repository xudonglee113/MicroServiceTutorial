using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op)
        {

        }

        public DbSet<Platform> Platforms { get; set; }

        
        
    }
}