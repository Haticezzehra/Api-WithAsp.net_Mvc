using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models.Domain;

namespace ProjectMVC.Data
{
    public class ForexDbContext : DbContext
    {
        public ForexDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Forex> Forex{ get; set; }
    }
}
    