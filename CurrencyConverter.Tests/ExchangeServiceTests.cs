using Microsoft.EntityFrameworkCore;
using CurrencyConverter.Infrastructure.Data;
using CurrencyConverter.Infrastructure.Entities;
using CurrencyConverter.Infrastructure.Services;
using System.Text.Json;

namespace CurrencyConverter.Tests
{
    public class ExchangeServiceTests
    {
        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task ConvertAsync_ReturnsFromCache_WhenRateIsFresh()
        {
            // Arrange
            var db = CreateDbContext();
            var now = DateTime.UtcNow;

            db.CurrencyRates.Add(new CurrencyRate
            {
                FromCurrency = "USD",
                ToCurrency = "EUR",
                Rate = 0.9m,
                FetchedAt = now
            });

            await db.SaveChangesAsync();

            var httpClient = new HttpClient(); // не используется, так как кэш сработает
            var service = new ExchangeService(db, httpClient);

            // Act
            var result = await service.ConvertAsync(100, "USD", "EUR");

            // Assert
            Assert.Equal(90, result.ConvertedAmount);
            Assert.Equal(0.9m, result.Rate);
            Assert.Equal("USD", result.From);
            Assert.Equal("EUR", result.To);
        }

        [Fact]
        public async Task ConvertAsync_FetchesFromApi_WhenNotCached()
        {
            var db = CreateDbContext();

            var json = """
                       {
                         "success": true,
                         "info": {
                           "rate": 0.85
                         },
                         "result": 85.0
                       }
                      """;

            var document = JsonDocument.Parse(json);
            var mockHandler = new MockHttpMessageHandler(document.RootElement);
            var httpClient = new HttpClient(mockHandler);

            var service = new ExchangeService(db, httpClient);

            var result = await service.ConvertAsync(100, "USD", "EUR");

            Assert.Equal(85, result.ConvertedAmount);
            Assert.Equal(0.85m, result.Rate);
            Assert.Equal("USD", result.From);
            Assert.Equal("EUR", result.To);

            var savedRate = await db.CurrencyRates.FirstOrDefaultAsync(x => x.FromCurrency == "USD" && x.ToCurrency == "EUR");
            Assert.NotNull(savedRate);
            Assert.Equal(0.85m, savedRate!.Rate);
        }

    }

}
