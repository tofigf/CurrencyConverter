using CurrencyConverter.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CurrencyRate> CurrencyRates => Set<CurrencyRate>();
        public DbSet<ConversionHistory> ConversionHistories => Set<ConversionHistory>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
