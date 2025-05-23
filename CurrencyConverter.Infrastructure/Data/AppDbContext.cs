using CurrencyConverter.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CurrencyRate> CurrencyRates => Set<CurrencyRate>();
        public DbSet<ConversionHistory> ConversionHistories => Set<ConversionHistory>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
