namespace CurrencyConverter.RestApi.Requests
{
    public record ConvertRequest(decimal Amount, string From, string To);
}
