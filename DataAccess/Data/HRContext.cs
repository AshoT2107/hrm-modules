using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class HRContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public HRContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Server=localhost;Port=5432;Database=cityfuel-hr;User Id=postgres;Password=shokhzod99;SslMode=Disable;Pooling=true;").UseSnakeCaseNamingConvention();
        }
    }
}
