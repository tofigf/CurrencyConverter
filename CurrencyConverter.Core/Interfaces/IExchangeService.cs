using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core.Interfaces
{
    public interface IExchangeService
    {
        Task<ExchangeResult> ConvertAsync(decimal amount, string fromCurrency, string toCurrency);
    }
}
