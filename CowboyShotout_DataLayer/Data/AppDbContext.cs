using CowboyShotout_DataLayer.Models.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CowboyShotout_DataLayer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() : base()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=tcp:cbso1.database.windows.net,1433;Initial Catalog=CowboyShotout_one;Persist Security Info=False;User ID=Nash;Password=Vaskemaskin45;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Position>().HasNoKey();
    }

    public DbSet<CowboyModel> CowboyModels { get; set; }
}