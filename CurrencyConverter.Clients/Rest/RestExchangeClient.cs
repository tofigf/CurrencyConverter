using CurrencyConverter.Core.Models;
using System.Net.Http.Json;

namespace CurrencyConverter.Clients.Rest
{
    public class RestExchangeClient
    {
        private readonly HttpClient _client;
        public RestExchangeClient(HttpClient client) => _client = client;

        public async Task<ExchangeResult> ConvertAsync(decimal amount, string from, string to)
        {
            var response = await _client.PostAsJsonAsync("/convert", new { amount, from, to });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ExchangeResult>()
                   ?? throw new InvalidOperationException("Invalid response from REST API.");
        }
    }
}
