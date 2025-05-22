using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Core.Models;
using CurrencyConverter.Infrastructure.Data;
using CurrencyConverter.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;

namespace CurrencyConverter.Infrastructure.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _http;

        public ExchangeService(AppDbContext context, HttpClient http)
        {
            _dbContext = context;
            _http = http;
        }

        public async Task<ExchangeResult> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            var now = DateTime.UtcNow;
            var cached = await _dbContext.CurrencyRates.FirstOrDefaultAsync(x =>
                x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency &&
                now - x.FetchedAt < TimeSpan.FromHours(1));

            decimal rate;

            if (cached != null)
            {
                rate = cached.Rate;
            }
            else
            {
                var url = $"https://api.exchangerate.host/convert?from={fromCurrency}&to={toCurrency}";
                var response = await _http.GetFromJsonAsync<JsonElement>(url);
                rate = response.GetProperty("info").GetProperty("rate").GetDecimal();

                _dbContext.CurrencyRates.Add(new CurrencyRate
                {
                    FromCurrency = fromCurrency,
                    ToCurrency = toCurrency,
                    Rate = rate,
                    FetchedAt = now
                });
            }

            var result = amount * rate;

            _dbContext.ConversionHistories.Add(new ConversionHistory
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Amount = amount,
                Rate = rate,
                Result = result,
                Timestamp = now
            });

            await _dbContext.SaveChangesAsync();

            return new ExchangeResult(amount, fromCurrency, toCurrency, rate, result, now);
        }
    }
}
