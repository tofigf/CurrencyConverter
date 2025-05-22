namespace CurrencyConverter.Infrastructure.Entities
{
    public class ConversionHistory
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = default!;
        public string ToCurrency { get; set; } = default!;
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
