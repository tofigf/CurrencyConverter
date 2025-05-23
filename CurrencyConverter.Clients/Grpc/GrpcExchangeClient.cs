using CurrencyConverter.Core.Models;
using CurrencyConverter.Grpc;


namespace CurrencyConverter.Clients.Grpc
{
    public class GrpcExchangeClient
    {
        private readonly ExchangeConverter.ExchangeConverterClient _client;
        public GrpcExchangeClient(ExchangeConverter.ExchangeConverterClient client) => _client = client;

        public async Task<ExchangeResult> ConvertAsync(decimal amount, string from, string to)
        {
            var reply = await _client.ConvertAsync(new ConvertRequest
            {
                Amount = (double)amount,
                From = from,
                To = to
            });

            return new ExchangeResult(amount, from, to, (decimal)reply.Rate, (decimal)reply.ConvertedAmount, DateTime.Parse(reply.Timestamp));
        }
    }
}
