using CowboyShotout_DataLayer.Models.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CowboyShotout_DataLayer.Data
{
    public class CowboyDbContext : DbContext
    {
        public CowboyDbContext(DbContextOptions<CowboyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<CowboyModel> CowboyModels { get; set; }
    }
}