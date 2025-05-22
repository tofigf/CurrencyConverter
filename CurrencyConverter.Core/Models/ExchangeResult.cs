namespace CurrencyConverter.Core.Models
{
    public record ExchangeResult(
        decimal OriginalAmount,
        string From,
        string To,
        decimal Rate,
        decimal ConvertedAmount,
        DateTime Timestamp
    );

}
